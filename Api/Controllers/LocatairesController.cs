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
    public class LocatairesController : ControllerBase
    {
        private readonly ApiContext _context;

        public LocatairesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Locataires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locataire>>> GetLocataire()
        {
            return await _context.Locataire.ToListAsync();
        }

        // GET: api/Locataires/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Locataire>> GetLocataire(int id)
        {
            var locataire = await _context.Locataire.FindAsync(id);

            if (locataire == null)
            {
                return NotFound();
            }

            return locataire;
        }

        // PUT: api/Locataires/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocataire(int id, Locataire locataire)
        {
            if (id != locataire.Id)
            {
                return BadRequest();
            }

            _context.Entry(locataire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocataireExists(id))
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

        // POST: api/Locataires
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Locataire>> PostLocataire(Locataire locataire)
        {
            _context.Locataire.Add(locataire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocataire", new { id = locataire.Id }, locataire);
        }

        // DELETE: api/Locataires/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Locataire>> DeleteLocataire(int id)
        {
            var locataire = await _context.Locataire.FindAsync(id);
            if (locataire == null)
            {
                return NotFound();
            }

            _context.Locataire.Remove(locataire);
            await _context.SaveChangesAsync();

            return locataire;
        }

        private bool LocataireExists(int id)
        {
            return _context.Locataire.Any(e => e.Id == id);
        }
    }
}
