using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDeVoitures.Models
{
    public class Filter
    {
        public int Start { get; set; } = 0;

        public int Count { get; set; } = 10;

        public int? MinPrice { get; set; }

        public int? MaxPrice { get; set; }

        public Transmission? Transmission { get; set; }

        public FuelType? FuelType { get; set; }
    }
}
