using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public String Username { get; set; }

        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public Civilite Civilite { get; set; }

        public String Prenom { get; set; }
        
        public String Nom{ get; set; }

        public String CodePays { get; set; }

        public String Adresse { get; set; }

        public String CodePostal { get; set; }

        public String Ville { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateDeNaissance { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
