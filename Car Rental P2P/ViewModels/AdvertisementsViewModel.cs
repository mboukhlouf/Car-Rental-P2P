using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalP2P.Models.Api;
using CarRentalP2P.Models;

namespace CarRentalP2P.ViewModels
{
    public class AdvertisementsViewModel : BaseViewModel
    {
        public int CurrentPage { get; set; } = 1;
        public int MaxPage { get; set; } = 100;
        public Filter filter { set; get; }
        public IEnumerable<Advertisement> Advertisements { get; set; }
        
    }
}
