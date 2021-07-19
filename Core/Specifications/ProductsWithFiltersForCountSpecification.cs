using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecParams productSpecParams) : base(x =>
            (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search)) &&
            (string.IsNullOrEmpty(productSpecParams.BrandName) || x.Brand.Name.ToLower().Contains(productSpecParams.Search)) &&
            (!productSpecParams.CategoryId.HasValue) || x.Categories.Select(c => c.Id).FirstOrDefault() == productSpecParams.CategoryId)
        {
        }
    }
}
