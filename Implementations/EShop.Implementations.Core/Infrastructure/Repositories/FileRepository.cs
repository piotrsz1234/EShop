using EShop.Core.Entities;
using EShop.Core.Infrastructure.Repositories;
using EShop.Implementations.EF.Contexts;

namespace EShop.Implementations.Core.Infrastructure.Repositories
{
    internal sealed class FileRepository : RepositoryGenericBase<File>, IFileRepository
    {
        public FileRepository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}