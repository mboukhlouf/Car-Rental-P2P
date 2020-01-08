using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LocationDeVoitures.Helpers;
using LocationDeVoitures.Models;
using LocationDeVoitures.Models.Api;
using LocationDeVoitures.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocationDeVoitures.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        // GET: Login
        public async Task<ActionResult> Login()
        {
            using var client = new ApiClient();
            User user = null;
            if (Request.Cookies.ContainsKey("token"))
            {
                String token = Request.Cookies["token"];
                client.Token = token;
                user = await client.GetUser();
            }

            if (user != null)
            {
                RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        // POST: Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel auth)
        {
            using var client = new ApiClient();
            bool result = await client.GetToken(auth.Authentication);
            if (result)
            {
                Response.Cookies.Append("token", client.Token);
                return RedirectToAction("Index", "Advertisements");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Register
        public async Task<ActionResult> Register()
        {
            using var client = new ApiClient();
            User user = null;
            if (Request.Cookies.ContainsKey("token"))
            {
                String token = Request.Cookies["token"];
                client.Token = token;
                user = await client.GetUser();
            }

            if (user != null)
            {
                RedirectToAction("Index", "Home");
            }
            return View(new RegistrationViewModel());
        }

        // POST: Login
        [HttpPost]
        public ActionResult Register(RegistrationViewModel registrationViewModel)
        {
            return RedirectToAction();
        }

        // GET: Logout
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Authentication");
        }
    }
}