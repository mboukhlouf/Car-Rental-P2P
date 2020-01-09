using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalP2P.Models.Api;
using CarRentalP2P.Models;

namespace CarRentalP2P.BindingModels
{
    public class AdvertisementsBindingModel
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }

        public int Count { get; set; }
    }
}
