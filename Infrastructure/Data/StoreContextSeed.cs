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
            // Seed data contained in the json file if the database is empty.
            try
            {
                if (!context.Brands.Any() && !context.Categories.Any() && !context.Products.Any())
                {
                    var brandsJson = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<Brand>>(brandsJson);

                    var categoriesJson = File.ReadAllText("../Infrastructure/Data/SeedData/categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesJson);

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
                            ImageUrl = productTemp.ImageUrl,
                            Rating = productTemp.Rating,
                            BrandId = productTemp.BrandId,
                            Categories = new List<Category>(),
                        };

                        product.Brand = brands.Where(x => x.Id == product.BrandId).FirstOrDefault();

                        foreach (var categoryId in productTemp.CategoryIds)
                        {
                            product.Categories.Add(categories.Where(x=>x.Id == categoryId).FirstOrDefault());
                        }

                        products.Add(product);
                    }


                    // Add seed objects to the database
                    // Id values need to be reset to zero because SQL Server will assign them automatically.
                    foreach (var brand in brands)
                    {
                        brand.Id = 0;
                        context.Brands.Add(brand);
                    }

                    foreach (var category in categories)
                    {
                        category.Id = 0;
                        context.Categories.Add(category);
                    }

                    foreach (var product in products)
                    {
                        // product.id is not assigned by the json file, therefore no need to reset the id value.
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

        // This ProductJson class maps directly to the objects in the products.json file.
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

            public int BrandId { get; set; }
            public IList<int> CategoryIds { get; set; } = new List<int>();
        }
    }
}
