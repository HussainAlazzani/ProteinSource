using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsWithCategoryAndBrandSpecification : BaseSpecification<Product>
    {
        /// <summary>
        /// The product parameters are passed to the base class to be added to the criteria.
        /// </summary>
        /// <param name="productParams">The product search and filter terms</param>
        public ProductsWithCategoryAndBrandSpecification(ProductSpecParams productParams) : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (string.IsNullOrEmpty(productParams.BrandName) || x.Brand.Name.ToLower().Contains(productParams.BrandName)) &&
            (!productParams.CategoryId.HasValue || x.Categories.Select(c => c.Id).FirstOrDefault() == productParams.CategoryId))            
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Categories);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort.ToLower())
                {
                    case "a":
                        AddOrderBy(p => p.Price);
                        break;
                    case "d":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }
    }
}
