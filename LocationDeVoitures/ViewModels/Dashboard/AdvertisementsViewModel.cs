using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LocationDeVoitures.Models;
using LocationDeVoitures.Models.Api;

namespace LocationDeVoitures.ViewModels.Dashboard
{
    public class AdvertisementsViewModel : BaseViewModel
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }
        
    }
}
