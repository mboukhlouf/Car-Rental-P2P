using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalP2P.Models.Api;

namespace CarRentalP2P.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {
        public Advertisement Advertisement { get; set; }

        public Reservation Reservation { get; set; }

    }
}
