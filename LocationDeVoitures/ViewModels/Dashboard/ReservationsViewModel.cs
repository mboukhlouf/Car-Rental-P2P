using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LocationDeVoitures.Models;
using LocationDeVoitures.Models.Api;

namespace LocationDeVoitures.ViewModels.Dashboard
{
    public class ReservationsViewModel : BaseViewModel
    {
        public IEnumerable<Reservation> Reservations { get; set; }
        
    }
}
