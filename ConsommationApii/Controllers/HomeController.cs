using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConsommationApii.Models;
using Api.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ConsommationApii.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        string Baseurl = "https://localhost:44325";

        public async Task<ActionResult> Index()
        {
            
                  List < Annonce > EmpInfo = new List<Annonce>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Annonces1");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<List<Annonce>>(EmpResponse);

                }

                return View(EmpInfo);
            }
        }
        [HttpGet]
        public ActionResult create() { return View(); }
        [HttpPost]
        public ActionResult create(Annonce annonce)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var message = client.PostAsJsonAsync("api/Annonece1", annonce).Result;
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44325/api/");
                var deleteTask = client.DeleteAsync("Annonces1/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode) { return RedirectToAction("Index"); }
            }
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {
            Annonce annonce = null; using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44325/api/");
                var responseTask = client.GetAsync("Annonces1?id=" + id.ToString());
                responseTask.Wait(); var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Annonce>();
                    readTask.Wait();
                    annonce = readTask.Result;
                }
            }
            return View(annonce);
        }
        [HttpPost]
        public ActionResult Edit(Annonce annonce)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44325/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var putTask = client.PutAsJsonAsync<Annonce>("Annonces1", annonce);
            putTask.Wait(); var result = putTask.Result;
            if (result.IsSuccessStatusCode) { return RedirectToAction("Index"); }
            return View(annonce);
        }




    }
}