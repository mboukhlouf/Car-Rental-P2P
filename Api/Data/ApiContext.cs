using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext()
        {

        }

        public ApiContext (DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Advertisement> Advertisement { get; set; }

        public DbSet<Reservation> Reservation { get; set; }

        public DbSet<ReservationMessage> ReservationMessage { get; set; }
    }
}
