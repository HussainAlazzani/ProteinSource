using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string Flavour { get; set; }
        public int Weight { get; set; }
        public decimal Price { get; set; }
        public decimal PreviousPrice { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string ImageUrl { get; set; }

        public Brand Brand { get; set; }
        public int BrandId { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
