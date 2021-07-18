using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class CategoriesWithProductsSpecification : BaseSpecification<Category>
    {
        public CategoriesWithProductsSpecification()
        {
            AddInclude(x => x.Products);
        }
    }
}
