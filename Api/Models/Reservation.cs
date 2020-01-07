﻿using System;
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

        [Required]
        [DataType(DataType.Date)]
        public DateTime PickupDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DropOffDate { get; set; }

        public ReservationState State { get; set; }

        public DateTime CreatedAt { get; set; }

        public Advertisement Advertisement { get; set; }

        public User User { get; set; }

        public IEnumerable<ReservationMessage> Messages { get; set; }

        public Reservation()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
