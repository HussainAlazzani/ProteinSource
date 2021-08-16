using API.Dtos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class AsDtoExtensions
    {
        public static ProductDto AsDto(this Product product)
        {
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Flavour = product.Flavour,
                Weight = product.Weight,
                Price = product.Price,
                PreviousPrice = product.PreviousPrice,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Rating = product.Rating,

                Brand = product.Brand.Name,
                Categories = product.Categories.Select(x => x.Id).ToArray()
            };

            return productDto;
        }

        public static CategoryDto AsDto(this Category categories)
        {
            var categoryDto = new CategoryDto
            {
                Id = categories.Id,
                Name = categories.Name,
                ProductIds = categories.Products.Select(x => x.Id).ToArray(),
            };

            return categoryDto;
        }
    }
}
