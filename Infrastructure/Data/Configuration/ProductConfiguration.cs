using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Flavour);
            builder.Property(p => p.Weight);
            builder.Property(p => p.Price).HasColumnType("decimal(10,2)");
            builder.Property(p => p.PreviousPrice).HasColumnType("decimal(10,2)");
            builder.Property(p => p.Rating).IsRequired();
            builder.Property(p => p.PictureUrl);
            builder.Property(p => p.Description);

            builder
                .HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId);

            builder
                .HasMany(p => p.Categories)
                .WithMany(p => p.Products)
                .UsingEntity(j => j.ToTable("ProductCategories"));
        }
    }
}

