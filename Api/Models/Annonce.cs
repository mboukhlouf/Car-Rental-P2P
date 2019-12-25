using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Annonce
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.ImageUrl)]
        public Uri ImageUri { get; set; }

        public String Marque { get; set; }

        public String Modele { get; set; }

        public int Annee { get; set; }

        public int Kilométrage { get; set; }

        public PipeTransmissionMode Transmission { get; set; }

        public String NombrePortes { get; set; }

        public Locataire Locataire { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
