using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LocationDeVoitures.Helpers;
using LocationDeVoitures.Models;
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
        public ActionResult Login()
        {
           /* if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Home");
            }*/
            return View();
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
            return RedirectToAction("Index", "Authentication");
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Register(LoginViewModel auth)
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