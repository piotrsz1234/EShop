using System;

namespace EShop.Core.Entities.Interfaces
{
    public interface IEntity
    {
        long Id { get; set; }
        bool IsDeleted { get; set; }
        DateTime InsertDateUtc { get; set; }
        DateTime ModificationDateUtc { get; set; }
    }
}