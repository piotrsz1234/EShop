using EShop.Core.Entities.Interfaces;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    public abstract class RepositoryGenericBase<T> : IRepositoryGenericBase<T> where T : class, IEntity
    {
        protected readonly MainDbContext DbContext;

        public RepositoryGenericBase(MainDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T> GetOneAsync(long key, params Expression<Func<T, object>>[] includes)
        {
            var query = DbContext.Set<T>().AsQueryable();

            foreach (var include in includes) {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == key && x.IsDeleted == false);
        }

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = DbContext.Set<T>().AsQueryable();

            foreach (var include in includes) {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes)
        {
            var query = DbContext.Set<T>().AsQueryable();

            foreach (var include in includes) {
                query = query.Include(include);
            }

            return await query.Where(expression).ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = DbContext.Set<T>().AsQueryable();

            foreach (var include in includes) {
                query = query.Include(include);
            }

            return await query.ToArrayAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().AnyAsync(expression);
        }

        public async Task AddAsync(T entity)
        {
            if (entity.Id == 0) {
                DbContext.Set<T>().Add(entity);
            }

            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public void Remove(T entity)
        {
            entity.IsDeleted = true;
            entity.ModificationDateUtc = DateTime.UtcNow;
        }
    }
}