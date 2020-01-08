using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocationDeVoitures.Helpers;
using LocationDeVoitures.Models.Api;
using LocationDeVoitures.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LocationDeVoitures.Controllers
{
    public class ReservationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Reservation reservation)
        {
            using var client = new ApiClient();
            User user;
            client.Token = Request.Cookies["token"];
            user = await client.GetUserAsync();

            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            reservation.UserId = user.Id;
            var result = await client.AddReservationAsync(reservation);

            if(result != null)
            {
                return RedirectToAction("Details", new {id = result.Id});
            }

            return RedirectToAction("Details", "Advertisements", new {id = reservation.AdvertisementId});
        }

        public async Task<IActionResult> Details(int id)
        {
            using var client = new ApiClient();
            User user;
            client.Token = Request.Cookies["token"];
            user = await client.GetUserAsync();

            var advertisement = await client.GetAdvertisementAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }

            advertisement.Owner = await client.GetUserAsync(advertisement.OwnerId);
            return View(new BaseViewModel());
        }
    }
}