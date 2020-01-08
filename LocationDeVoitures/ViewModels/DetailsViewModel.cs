using LocationDeVoitures.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDeVoitures.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {
        public Advertisement Advertisement { get; set; }

        public Reservation Reservation { get; set; }

    }
}
