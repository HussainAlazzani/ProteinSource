using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        private class ProductJson
        {
            public string Name { get; set; }
            public string Flavour { get; set; }
            public int Weight { get; set; }
            public decimal Price { get; set; }
            public decimal PreviousPrice { get; set; }
            public string Description { get; set; }
            public int Rating { get; set; }
            public string ImageUrl { get; set; }

            //public Brand Brand { get; set; }
            public int BrandId { get; set; }

            //public ICollection<ProductCategory> CategoriesLink { get; set; } = new List<ProductCategory>();
            public IList<int> CategoryIds { get; set; } = new List<int>();
        }

        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.ImageUrl);
        }
    }
}
