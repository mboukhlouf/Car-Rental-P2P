using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LocationDeVoitures.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocationDeVoitures.Controllers
{
    public class AuthenticationController : Controller
    {
        //GET : /Authentication
        public ActionResult Index()
        {
            IEnumerable<User> usersList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("User").Result;
            usersList = response.Content.ReadAsAsync<IEnumerable<User>>().Result;

            return View(usersList);
        }



        public ActionResult Register(int id = 0)
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("User", user).Result;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}