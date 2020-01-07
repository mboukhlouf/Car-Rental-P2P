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

        [Required(ErrorMessage ="this field is requiered !")]
        public String Username { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        public Civility Civility { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        public String LastName{ get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        public String Countrycode { get; set; }

        public String Address { get; set; }

        public String ZipCode { get; set; }

        [Required(ErrorMessage = "this field is requiered !")]
        public String City { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }

        public User()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
