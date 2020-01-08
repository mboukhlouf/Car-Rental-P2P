using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LocationDeVoitures.Models;
using LocationDeVoitures.Models.Api;

namespace LocationDeVoitures.BindingModels
{
    public class AdvertisementsBindingModel
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }

        public int Count { get; set; }
    }
}
