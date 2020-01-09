using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalP2P.Helpers;
using CarRentalP2P.Models.Api;
using CarRentalP2P.ViewModels;
using CarRentalP2P.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalP2P.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            using var client = new ApiClient();
            User user;
            String token = Request.Cookies["token"];
            client.Token = token;
            user = await client.GetUserAsync();

            return View(new BaseViewModel
            {
                User = user
            });
        }

        public async Task<ActionResult> Team()
        {
            ViewBag.Message = "";

            using var client = new ApiClient();
            User user;
            String token = Request.Cookies["token"];
            client.Token = token;
            user = await client.GetUserAsync();

            return View(new BaseViewModel
            {
                User = user
            });
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "";

            using var client = new ApiClient();
            User user;
            String token = Request.Cookies["token"];
            client.Token = token;
            user = await client.GetUserAsync();

            return View(new BaseViewModel
            {
                User = user
            });
        }

        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "";

            using var client = new ApiClient();
            User user;
            String token = Request.Cookies["token"];
            client.Token = token;
            user = await client.GetUserAsync();

            return View(new BaseViewModel
            {
                User = user
            });
        }
    }
}