//using Core.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.Data.Configuration
//{
//    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
//    {
//        public void Configure(EntityTypeBuilder<ProductCategory> builder)
//        {
//            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });
//        }
//    }
//}