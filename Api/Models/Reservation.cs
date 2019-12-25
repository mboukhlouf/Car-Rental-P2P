using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateDeDebut { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateDeFin { get; set; }

        public Annonce Annonce { get; set; }

        public Client Client { get; set; }
    }
}
