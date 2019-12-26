using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext (DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public DbSet<Api.Models.Annonce> Annonce { get; set; }
        public DbSet<Api.Models.Client> clients { get; set; }
        public DbSet<Api.Models.Locataire> locataires { get; set; }
        public DbSet<Api.Models.Reservation> reservations { get; set; }
    }
}
