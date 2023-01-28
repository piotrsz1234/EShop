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
    }
}