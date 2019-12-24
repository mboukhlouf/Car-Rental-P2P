using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Client
    {
        public int Id { get; set; }
        
        public String Username { get; set; }
        public String Email { get; set; }
        
        public String Password { get; set; }

        public Civilite Civilite { get; set; }

        public String Prenom { get; set; }
        
        public String Nom{ get; set; }

        public Pays Pays { get; set; }

        public String Adresse { get; set; }

        public String CodePostal { get; set; }

        public String Ville { get; set; }

        public DateTime DateDeNaissance { get; set; }
    }
}
