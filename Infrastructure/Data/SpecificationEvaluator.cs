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
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var outputQuery = inputQuery;

            if (spec.Criteria != null)
            {
                outputQuery = outputQuery.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                outputQuery = outputQuery.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                outputQuery = outputQuery.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagingEnabled)
            {
                outputQuery = outputQuery.Skip(spec.Skip).Take(spec.Take);
            }

            outputQuery = spec.Includes.Aggregate(outputQuery, (current, include) =>
                current.Include(include));

            return outputQuery;
        }
    }
}
