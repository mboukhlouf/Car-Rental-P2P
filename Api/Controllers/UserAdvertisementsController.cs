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
    public class UserAdvertisementsController : ControllerBase
    {
        private readonly AdvertisementsRepository advertisementsRepository;

        public UserAdvertisementsController(ApiContext context)
        {
            advertisementsRepository = new AdvertisementsRepository(context);
        }


        // GET: api/UserAdvertisements/5
        [HttpGet("{ownerId}")]
        public async Task<ActionResult<IEnumerable<Advertisement>>> GetUserAdvertisements(int ownerId)
        {
            var advertisements = await advertisementsRepository.AsQueryable()
                .Where(ad => ad.OwnerId == ownerId).ToListAsync();
            return advertisements;
        }
    }
}
