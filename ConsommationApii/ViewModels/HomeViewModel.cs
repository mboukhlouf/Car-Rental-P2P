using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsommationApii.ViewModels
{
    public class HomeViewModel
    {
        public int CurrentPage { get; set; } = 1;
        public int MaxPage { get; set; } = 100;
        public IEnumerable<Annonce> Annonces { get; set; }
        
    }
}
