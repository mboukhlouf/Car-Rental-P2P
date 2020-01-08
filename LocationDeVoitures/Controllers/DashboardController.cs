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
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Advertisements");
        }

        public async Task<IActionResult> Advertisements()
        {
            using var client = new ApiClient();
            User user;
            client.Token = Request.Cookies["token"]; ;
            user = await client.GetUserAsync();

            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var advertisements = await client.GetUserAdvertisementsAsync(user.Id);
            return View(new ViewModels.Dashboard.AdvertisementsViewModel()
            {
                User = user,
                Advertisements = advertisements
            });
        }

        public async Task<IActionResult> Reservations()
        {
            using var client = new ApiClient();
            User user;
            client.Token = Request.Cookies["token"];
            user = await client.GetUserAsync();

            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var reservations = await client.GetUserReservationsAsync(user.Id);
            return View(new ViewModels.Dashboard.ReservationsViewModel()
            {
                User = user,
                Reservations = reservations
            });
        }
    }
}