using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Flavour { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal PreviousPrice { get; set; }
        public string PictureUrl { get; set; }
        public int Rating { get; set; }

        public string Brand { get; set; }
        public int[] Categories { get; set; }
    }
}
