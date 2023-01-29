using EShop.Core.Entities.Interfaces;
using System.Linq.Expressions;

namespace EShop.Core.Infrastructure.Repositories
{
    public interface IRepositoryGenericBase<T> where T: class, IEntity
    {
        Task<T> GetOneAsync(long key, params Expression<Func<T, object>>[] includes);
        Task<T> GetOneAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes);
        
        IReadOnlyCollection<T> GetAll(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes);
        
        IReadOnlyCollection<T> GetAll(params Expression<Func<T, object>>[] includes);
        
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task SaveChangesAsync();
        void Remove(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    }
}