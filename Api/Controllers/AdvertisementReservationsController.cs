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
    public class AdvertisementReservationsController : ControllerBase
    {
        private readonly ReservationsRepository resveReservationsRepository;

        public AdvertisementReservationsController(ApiContext context)
        {
            resveReservationsRepository = new ReservationsRepository(context);
        }


        // GET: api/AdvertisementReservations/5
        [HttpGet("{advertisementId}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAdvertisementReservations(int advertisementId)
        {
            var reservations = await resveReservationsRepository.AsQueryable()
                .Where(res => res.AdvertisementId == advertisementId).ToListAsync();
            return reservations;
        }
    }
}
