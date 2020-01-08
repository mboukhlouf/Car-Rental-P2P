using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDeVoitures.ViewModels
{
    public class ReservationViewModel : BaseViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime PickupDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DropOffDate { get; set; }
    }

}
