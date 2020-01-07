using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.BindingModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Api.Models;
using Api.Data;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly ApiContext _context;

        public AdvertisementsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Advertisements
        [HttpGet]
        public async Task<ActionResult<AdvertisementsBindingModel>> GetAdvertisement(Filter filter)
        {
            var ads = _context.Advertisement.AsQueryable();

            // Price filter
            if (filter.MinPrice != null && filter.MaxPrice != null)
            {
                ads = ads.Where(ad => ad.Price >= filter.MinPrice && ad.Price <= filter.MaxPrice);
            }

            // Transmission filter
            if (filter.Transmission != null)
            {
                ads = ads.Where(ad => ad.Transmission == filter.Transmission);
            }

            // Fuel Type filter
            if (filter.FuelType != null)
            {
                ads = ads.Where(ad => ad.FuelType == filter.FuelType);
            }

            // Order by CreationDate
            ads = ads.OrderByDescending(ad => ad.CreationTime);

            int totalCount = ads.Count();

            ads = ads.Skip(filter.Start)
                .Take(filter.Count);

            return new AdvertisementsBindingModel
            {
                Count = totalCount,
                Advertisements = await ads.ToListAsync()
            };
        }

        // GET: api/Advertisements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Advertisement>> GetAdvertisement(int id)
        {
            var advertisement = await _context.Advertisement.FindAsync(id);

            if (advertisement == null)
            {
                return NotFound();
            }

            return advertisement;
        }

        // PUT: api/Advertisements/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvertisement(int id, Advertisement advertisement)
        {
            if (id != advertisement.Id)
            {
                return BadRequest();
            }

            _context.Entry(advertisement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertisementExists(id))
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

        // POST: api/Advertisements
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Advertisement>> PostAdvertisement(Advertisement advertisement)
        {
            _context.Advertisement.Add(advertisement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdvertisement", new { id = advertisement.Id }, advertisement);
        }

        // DELETE: api/Advertisements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Advertisement>> DeleteAdvertisement(int id)
        {
            var advertisement = await _context.Advertisement.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }

            _context.Advertisement.Remove(advertisement);
            await _context.SaveChangesAsync();

            return advertisement;
        }

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisement.Any(e => e.Id == id);
        }
    }
}
