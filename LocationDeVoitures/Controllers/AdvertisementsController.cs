using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using LocationDeVoitures.BindingModels;
using LocationDeVoitures.Helpers;
using LocationDeVoitures.Models;
using LocationDeVoitures.ViewModels;

namespace LocationDeVoitures.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly ILogger<AdvertisementsController> _logger;

        public AdvertisementsController(ILogger<AdvertisementsController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            int count = 10;
            AdvertisementsBindingModel responseModel = new AdvertisementsBindingModel();
            using (var client = new HttpClient())
            {
                var filter = new Filter
                {
                    Start = 0,
                    Count = count
                };

                HttpRequestMessage message = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = ApiHelper.AdvertisementsUrl,
                    Content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json")
                };

                using (message)
                {
                    using (var response = await client.SendAsync(message))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseBody = response.Content.ReadAsStringAsync().Result;
                            responseModel = JsonConvert.DeserializeObject<AdvertisementsBindingModel>(responseBody);
                        }
                    }
                }
            }

            HomeViewModel model = new HomeViewModel
            {
                CurrentPage = 1,
                MaxPage = (responseModel.Count / count) + 1,
                Advertisements = responseModel.Advertisements
            };
            return View(model);
        }
    }
}