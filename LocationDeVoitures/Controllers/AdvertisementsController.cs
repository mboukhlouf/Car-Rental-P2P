using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using LocationDeVoitures.Models.Api;
using LocationDeVoitures.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Localization;
namespace LocationDeVoitures.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly ILogger<AdvertisementsController> _logger;
        private readonly IStringLocalizer<AdvertisementsController> _localizer;
        private readonly IHostingEnvironment _environment;

        public AdvertisementsController(IStringLocalizer<AdvertisementsController> localizer, IHostingEnvironment environment)
        {
            IStringLocalizer<AdvertisementsController> _localizer = localizer;
            _environment = environment;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public async Task<ActionResult> Index(Filter filter,int page=1)
        {

            using var client = new ApiClient();
            User user;
            client.Token = Request.Cookies["token"]; ;
            user = await client.GetUserAsync();

            int count = 10;
            /*
            var filter = new Filter
            {
            Start = (page - 1) * count,
            Count = count
            };
            */
            filter.Count = count;
            filter.Start = (page - 1) * count;
            var responseModel = await client.GetAdvertisementsAsync(filter);
            if (responseModel == null)
            {
                responseModel = new AdvertisementsBindingModel();
            }

            AdvertisementsViewModel model = new AdvertisementsViewModel
            {
                User = user,
                //CurrentPage = page,
                MaxPage = (responseModel.Count / count) + 1,
                Advertisements = responseModel.Advertisements
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Post()
        {
            using var client = new ApiClient();
            User user;
            client.Token = Request.Cookies["token"]; ;
            user = await client.GetUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            return View(new AdvertisementViewModel
            {
                User = user
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post(AdvertisementViewModel advertisementViewModel)
        {
            using var client = new ApiClient();
            User user;
            client.Token = Request.Cookies["token"]; ;
            user = await client.GetUserAsync();
            if (user == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            String imageUri;
            try
            {
                imageUri = UploadImage(advertisementViewModel.ImageFile);
            }
            catch (Exception e)
            {
                imageUri = "/img/default.png";
            }

            Advertisement advertisement = new Advertisement()
            {
                Brand = advertisementViewModel.Brand,
                Description = advertisementViewModel.Description,
                FuelType = advertisementViewModel.FuelType,
                Mileage = advertisementViewModel.Mileage,
                Model = advertisementViewModel.Model,
                NumberDoors = advertisementViewModel.NumberDoors,
                Price = advertisementViewModel.Price,
                Title = advertisementViewModel.Title,
                Year = advertisementViewModel.Year,
                Transmission = advertisementViewModel.Transmission,
                ImageUri = imageUri,
                IsActive = true,
                OwnerId = user.Id
            };

            bool registrationResult = await client.CreateAdvertisementAsync(advertisement);
            if (registrationResult)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction();
            }
        }

        public String UploadImage(IFormFile image)
        {
            String fileName = Path.GetFileNameWithoutExtension(image.FileName) + "_" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + Path.GetExtension(image.FileName);
            String folder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            String path = Path.Combine(folder, fileName);
            String uri = "/uploads/" + fileName;
            using (FileStream fs = System.IO.File.Create(path))
            {
                image.CopyTo(fs);
            }

            return uri;
        }

        public async Task<IActionResult> Details(int id)
        {
            using var client = new ApiClient();
            User user;
            client.Token = Request.Cookies["token"]; ;
            user = await client.GetUserAsync();

            var advertisement = await client.GetAdvertisementAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }

            advertisement.Owner = await client.GetUserAsync(advertisement.OwnerId);
            return View(new DetailsViewModel
            {
                User = user,
                Advertisement = advertisement,
                Reservation = new Reservation
                {
                    AdvertisementId = advertisement.Id
                }
            });
        }
    }
}