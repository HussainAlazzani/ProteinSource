using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity: BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> spec)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            var evaluatedQuery = SpecificationEvaluator<TEntity>.GetQuery(query, spec);

            return await evaluatedQuery.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetWithSpecAsync(ISpecification<TEntity> spec)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            var evaluateQuery = SpecificationEvaluator<TEntity>.GetQuery(query, spec);

            return await evaluateQuery.FirstOrDefaultAsync();
        }
    }
}
