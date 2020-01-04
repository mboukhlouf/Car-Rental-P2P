using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Api.Models;
using Api.Data;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationMessagesController : ControllerBase
    {
        private readonly ApiContext _context;

        public ReservationMessagesController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/ReservationMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationMessage>>> GetReservationMessage()
        {
            return await _context.ReservationMessage.ToListAsync();
        }

        // GET: api/ReservationMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationMessage>> GetReservationMessage(int id)
        {
            var reservationMessage = await _context.ReservationMessage.FindAsync(id);

            if (reservationMessage == null)
            {
                return NotFound();
            }

            return reservationMessage;
        }

        // PUT: api/ReservationMessages/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservationMessage(int id, ReservationMessage reservationMessage)
        {
            if (id != reservationMessage.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservationMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationMessageExists(id))
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

        // POST: api/ReservationMessages
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ReservationMessage>> PostReservationMessage(ReservationMessage reservationMessage)
        {
            _context.ReservationMessage.Add(reservationMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservationMessage", new { id = reservationMessage.Id }, reservationMessage);
        }

        // DELETE: api/ReservationMessages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservationMessage>> DeleteReservationMessage(int id)
        {
            var reservationMessage = await _context.ReservationMessage.FindAsync(id);
            if (reservationMessage == null)
            {
                return NotFound();
            }

            _context.ReservationMessage.Remove(reservationMessage);
            await _context.SaveChangesAsync();

            return reservationMessage;
        }

        private bool ReservationMessageExists(int id)
        {
            return _context.ReservationMessage.Any(e => e.Id == id);
        }
    }
}
