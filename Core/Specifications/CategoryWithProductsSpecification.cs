using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class CategoryWithProductsSpecification: BaseSpecification<Category>
    {
        public CategoryWithProductsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Products);
        }
    }
}
