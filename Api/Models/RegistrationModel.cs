using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class RegistrationModel
    {
        [Required]
        [StringLength(7, MinimumLength = 4, ErrorMessage = "the username must be ")]
        public String Username { get; set; }

        [Required(ErrorMessage = "this field is required !")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required(ErrorMessage = "this field is required !")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required(ErrorMessage = "this field is required !")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The confirmation password does not match")]
        public String PasswordConfirmation { get; set; }

        [Required(ErrorMessage = "this field is required !")]
        public Civility Civility { get; set; }

        [Required(ErrorMessage = "this field is required !")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "this field is required !")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "this field is required !")]
        public String Countrycode { get; set; }

        public String Address { get; set; }

        public String ZipCode { get; set; }

        [Required(ErrorMessage = "this field is required !")]
        public String City { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
