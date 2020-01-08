using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDeVoitures.Models.Api
{
    public class Advertisement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public String Title { get; set; }

        public String Description { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public String ImageUri { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        public String Brand { get; set; }

        [Required]
        public String Model { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public Transmission Transmission { get; set; }

        [Required]
        public int NumberDoors { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public Advertisement()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
