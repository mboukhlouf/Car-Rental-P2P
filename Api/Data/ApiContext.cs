using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext (DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Api.Models.Client> Client { get; set; }

        public DbSet<Api.Models.Annonce> Annonce { get; set; }

        public DbSet<Api.Models.Reservation> Reservation { get; set; }

        public DbSet<Api.Models.Locataire> Locataire { get; set; }
    }
}
