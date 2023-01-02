using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;
using System.Linq.Expressions;
using File = EShop.Core.Entities.File;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    internal sealed class FileRepository : RepositoryGenericBase<File>, IFileRepository
    {
        public FileRepository(MainDbContext dbContext) : base(dbContext)
        {
        }

        Task IRepositoryGenericBase<EShop.Core.Entities.File>.AddAsync(EShop.Core.Entities.File entity)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepositoryGenericBase<EShop.Core.Entities.File>.AnyAsync(Expression<Func<EShop.Core.Entities.File, bool>> expression)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyCollection<EShop.Core.Entities.File>> IRepositoryGenericBase<EShop.Core.Entities.File>.GetAllAsync(Expression<Func<EShop.Core.Entities.File, bool>> expression, params Expression<Func<EShop.Core.Entities.File, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyCollection<EShop.Core.Entities.File>> IRepositoryGenericBase<EShop.Core.Entities.File>.GetAllAsync(params Expression<Func<EShop.Core.Entities.File, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        Task<EShop.Core.Entities.File> IRepositoryGenericBase<EShop.Core.Entities.File>.GetOneAsync(long key, params Expression<Func<EShop.Core.Entities.File, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        Task<EShop.Core.Entities.File> IRepositoryGenericBase<EShop.Core.Entities.File>.GetOneAsync(Expression<Func<EShop.Core.Entities.File, bool>> predicate, params Expression<Func<EShop.Core.Entities.File, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        void IRepositoryGenericBase<EShop.Core.Entities.File>.Remove(EShop.Core.Entities.File entity)
        {
            throw new NotImplementedException();
        }

        Task IRepositoryGenericBase<EShop.Core.Entities.File>.SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}