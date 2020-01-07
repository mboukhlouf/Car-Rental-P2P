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
        private readonly AdvertisementsRepository advertisementsRepository;

        public AdvertisementsController(ApiContext context)
        {
            advertisementsRepository = new AdvertisementsRepository(context);
        }

        // GET: api/Advertisements
        [HttpGet]
        public async Task<ActionResult<AdvertisementsBindingModel>> GetAdvertisement(Filter filter)
        {
            return await advertisementsRepository.ListAsync(filter);
        }

        // GET: api/Advertisements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Advertisement>> GetAdvertisement(int id)
        {
            var advertisement = await advertisementsRepository.GetByIdAsync(id);

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

            try
            {
                await advertisementsRepository.EditAsync(advertisement);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!advertisementsRepository.Exists(id))
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
            await advertisementsRepository.AddAsync(advertisement);

            return CreatedAtAction("GetAdvertisement", new { id = advertisement.Id }, advertisement);
        }

        // DELETE: api/Advertisements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Advertisement>> DeleteAdvertisement(int id)
        {
            var advertisement = await advertisementsRepository.DeleteAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return advertisement;
        }
    }
}
