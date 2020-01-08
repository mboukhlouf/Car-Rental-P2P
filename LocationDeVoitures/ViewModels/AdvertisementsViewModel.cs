using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LocationDeVoitures.Models;

namespace LocationDeVoitures.ViewModels
{
    public class AdvertisementsViewModel : BaseViewModel
    {
        public int CurrentPage { get; set; } = 1;
        public int MaxPage { get; set; } = 100;
        public IEnumerable<Advertisement> Advertisements { get; set; }
        
    }
}
