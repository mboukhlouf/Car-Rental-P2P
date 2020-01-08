using LocationDeVoitures.Models.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDeVoitures.ViewModels
{
    public class AdvertisementViewModel : BaseViewModel
    {
        [Required]
        public String Title { get; set; }

        public String Description { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public Uri ImageUri { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        public String Brand { get; set; }

        [Required]
        public String Model { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public Transmission Transmission { get; set; }

        [Required]
        public int NumberDoors { get; set; }

        public bool IsActive { get; set; }


    }
}
