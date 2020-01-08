using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocationDeVoitures.Helpers;
using LocationDeVoitures.Models;
using LocationDeVoitures.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LocationDeVoitures.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using var client = new ApiClient();
            User user = null;
            if (Request.Cookies.ContainsKey("token"))
            {
                String token = Request.Cookies["token"];
                client.Token = token;
                user = await client.GetUser();
            }
            return View(new BaseViewModel
            {
                User = user
            });
        }
    }
}