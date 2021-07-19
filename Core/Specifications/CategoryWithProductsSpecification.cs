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

        public CategoryWithProductsSpecification(CategorySpecParams categoryParams) : base(x => x.Id == categoryParams.CategoryId)
        {
            AddInclude(x => x.Products);
            ApplyPaging(categoryParams.PageSize * (categoryParams.PageIndex - 1), categoryParams.PageSize);

            if (!string.IsNullOrEmpty(categoryParams.Sort))
            {
                switch (categoryParams.Sort.ToLower())
                {
                    case "a":
                        AddOrderBy(c => c.Products);
                        break;
                    case "d":
                        AddOrderByDescending(c => c.Products);
                        break;
                    default:
                        AddOrderBy(n => n.Products);
                        break;
                }
            }
        }
    }
}
