using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Brand: BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        //public ICollection<Product> Products { get; set; }
    }
}