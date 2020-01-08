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
    public class UserReservationsController : ControllerBase
    {
        private readonly ReservationsRepository resveReservationsRepository;

        public UserReservationsController(ApiContext context)
        {
            resveReservationsRepository = new ReservationsRepository(context);
        }


        // GET: api/UserReservations/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetUserAdvertisements(int userId)
        {
            var reservations = await resveReservationsRepository.AsQueryable()
                .Where(res => res.UserId == userId).ToListAsync();
            return reservations;
        }
    }
}
