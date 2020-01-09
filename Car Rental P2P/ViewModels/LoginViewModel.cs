using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalP2P.Models.Api;
using CarRentalP2P.Models;

namespace CarRentalP2P.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public AuthenticationModel Authentication { get; set; }
    }
}
