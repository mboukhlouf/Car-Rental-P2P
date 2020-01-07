using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocationDeVoitures.Models;

namespace LocationDeVoitures.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public AuthenticationModel Authentication { get; set; }
    }
}
