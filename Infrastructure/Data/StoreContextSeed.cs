using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            // Seed data contained the the json file if the database is empty.
            try
            {
                if (!context.Brands.Any() && !context.Categories.Any() && !context.Products.Any())
                {
                    var brandsJson = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<Brand>>(brandsJson);

                    foreach (var brand in brands)
                    {
                        context.Brands.Add(brand);
                    }

                    var categoriesJson = File.ReadAllText("../Infrastructure/Data/SeedData/categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesJson);

                    foreach (var category in categories)
                    {
                        context.Categories.Add(category);
                    }

                    var productsJson = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var productsTemp = JsonSerializer.Deserialize<List<ProductJson>>(productsJson);

                    var products = new List<Product>();

                    foreach (var productTemp in productsTemp)
                    {
                        var product = new Product
                        {
                            Name = productTemp.Name,
                            Flavour = productTemp.Flavour,
                            Weight = productTemp.Weight,
                            Price = productTemp.Price,
                            PreviousPrice = productTemp.PreviousPrice,
                            Description = productTemp.Description,
                            PictureUrl = productTemp.PictureUrl,
                            Rating = productTemp.Rating,
                            Brand = brands[productTemp.BrandId - 1],
                            Categories = new List<Category>(),
                        };

                        foreach (var categoryId in productTemp.CategoryIds)
                        {
                            product.Categories.Add(categories[categoryId - 1]);
                        }

                        context.Products.Add(product);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }

        private class ProductJson
        {
            public string Name { get; set; }
            public string Flavour { get; set; }
            public int Weight { get; set; }
            public decimal Price { get; set; }
            public decimal PreviousPrice { get; set; }
            public string Description { get; set; }
            public int Rating { get; set; }
            public string PictureUrl { get; set; }

            //public Brand Brand { get; set; }
            public int BrandId { get; set; }

            //public ICollection<ProductCategory> CategoriesLink { get; set; } = new List<ProductCategory>();
            public IList<int> CategoryIds { get; set; } = new List<int>();
        }
    }
}
