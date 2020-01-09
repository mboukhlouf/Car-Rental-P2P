using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CarRentalP2P.Models.Api;

namespace CarRentalP2P.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "this field is requiered !")]
        public String Username { get; set; }


        [Required(ErrorMessage = "this field is requiered !")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="the confirmation passeword doesn't match")]
        public String PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        public Civility Civility { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        public String Countrycode { get; set; }

        public String Address { get; set; }

        public String ZipCode { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        public String City { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}

