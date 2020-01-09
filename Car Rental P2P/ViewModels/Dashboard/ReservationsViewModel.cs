using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalP2P.Models.Api;
using CarRentalP2P.Models;

namespace CarRentalP2P.ViewModels.Dashboard
{
    public class ReservationsViewModel : BaseViewModel
    {
        public IEnumerable<Reservation> Reservations { get; set; }
        
    }
}
