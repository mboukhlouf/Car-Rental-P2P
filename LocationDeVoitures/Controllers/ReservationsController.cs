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

            return RedirectToAction("Details", "Reservations", new {id = reservation.AdvertisementId});
        }

        public async Task<IActionResult> Details(int id)
        {
            using var client = new ApiClient();
            User user;
            client.Token = Request.Cookies["token"];
            user = await client.GetUserAsync();

            var reservation = await client.GetReservationAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.Advertisement = await client.GetAdvertisementAsync(reservation.AdvertisementId);
            reservation.Advertisement.Owner = await client.GetUserAsync(reservation.Advertisement.OwnerId);
            if (user.Id != reservation.UserId && user.Id != reservation.Advertisement.OwnerId)
            {
                return NotFound();

            }
            return View(new ReservationDetailsViewModel
            {
                User = user,
                Advertisement = reservation.Advertisement,
                Reservation = reservation
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int reservationId, ReservationState status)
        {
            using var client = new ApiClient();
            User user;
            client.Token = Request.Cookies["token"];
            user = await client.GetUserAsync();

            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var reservation = await client.GetReservationAsync(reservationId);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.State = status;
            bool result = await client.UpdateReservation(reservation);
            return RedirectToAction("Details", "Reservations", new { id = reservation.Id });
        }
    }
}