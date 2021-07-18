using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithCategoriesAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithCategoriesAndBrandSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Categories);
        }
    }
}
