using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    /// <summary>
    /// Add the given specification to the given database query.
    /// </summary>
    /// <typeparam name="TEntity">The entity to be queried</typeparam>
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, ISpecification<TEntity> spec)
        {
            var q = query;

            if (spec.Criteria != null)
            {
                q = q.Where(spec.Criteria);
            }

            q = spec.Includes.Aggregate(q, (current, include) =>
                current.Include(include));

            return q;
        }
    }
}
