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
                user = await client.GetUserAsync();
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
            bool result = await client.GetTokenAsync(auth.Authentication);
            if (result)
            {
                Response.Cookies.Append("token", client.Token);
                return RedirectToAction("Index", "Advertisements");
            }
            return RedirectToAction("Index", "Authentication");
        }

        // GET: Register
        public async Task<ActionResult> Register()
        {
            using var client = new ApiClient();
            User user = null;
            client.Token = Request.Cookies["token"];
            user = await client.GetUserAsync();

            if (user != null)
            {
                RedirectToAction("Index", "Home");
            }
            return View(new RegistrationViewModel());
        }

        // POST: Login
        [HttpPost]
        public async Task<ActionResult> Register(RegistrationViewModel registrationViewModel)
        {
            using var client = new ApiClient();
            User user = new User
            {
                Username = registrationViewModel.Registration.Username,
                Password = registrationViewModel.Registration.Password,
                Email = registrationViewModel.Registration.Email,
                FirstName = registrationViewModel.Registration.FirstName,
                LastName = registrationViewModel.Registration.LastName,
                Civility = registrationViewModel.Registration.Civility,
                Countrycode = registrationViewModel.Registration.Countrycode,
                City = registrationViewModel.Registration.City,
                Address = registrationViewModel.Registration.Address,
                ZipCode = registrationViewModel.Registration.ZipCode,
                DateOfBirth = registrationViewModel.Registration.DateOfBirth
            };

            bool registrationResult = await client.CreateUserAsync(user);
            if (registrationResult)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction();
            }
        }

        // GET: Logout
        public ActionResult Logout()
        {
            Response.Cookies.Append("token", "", new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(-1)
            });
            return RedirectToAction("Login", "Authentication");
        }
    }
}