using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnoncesController : ControllerBase
    {
        private readonly ApiContext _context;

        public AnnoncesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Annonces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Annonce>>> GetAnnonce()
        {
            return await _context.Annonce.ToListAsync();
        }
        // GET: api/Annonces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Annonce>> GetAnnonce(int id)
        {
            var annonce = await _context.Annonce.FindAsync(id);

            if (annonce == null)
            {
                return NotFound();
            }

            return annonce;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnonce(int id, Annonce annonce)
        {
            if (id != annonce.Id)
            {
                return BadRequest();
            }

            _context.Entry(annonce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnonceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Annonces
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Annonce>> PostAnnonce(Annonce annonce)
        {
            _context.Annonce.Add(annonce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnnonce", new { id = annonce.Id }, annonce);
        }

        // DELETE: api/Annonces/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Annonce>> DeleteAnnonce(int id)
        {
            var annonce = await _context.Annonce.FindAsync(id);
            if (annonce == null)
            {
                return NotFound();
            }

            _context.Annonce.Remove(annonce);
            await _context.SaveChangesAsync();

            return annonce;
        }

        private bool AnnonceExists(int id)
        {
            return _context.Annonce.Any(e => e.Id == id);
        }
    }
}
