using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDeVoitures.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public String Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        public Civility Civility { get; set; }

        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName{ get; set; }

        [Required]
        public String Countrycode { get; set; }

        public String Address { get; set; }

        public String ZipCode { get; set; }

        [Required]
        public String City { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
