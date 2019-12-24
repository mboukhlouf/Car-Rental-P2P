using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Locataire : Client
    {
        private ICollection<Annonce> Annonces { get; set; }
    }
}
