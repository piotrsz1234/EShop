using EShop.Core.Entities.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using EShop.Core.Entities.Interfaces;

namespace EShop.Core.Entities
{
    public class File : IEntity
    {
        public long Id { get; set; }
        [SqlDefaultValue(DefaultValue = "(0)")]
        public bool IsDeleted { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime InsertDateUtc { get; set; }
        [SqlDefaultValue(DefaultValue = "GETUTCDATE()")]
        public DateTime ModificationDateUtc { get; set; }
        public string DisplayFileName { get; set; }
        public string DiscName { get; set; }
        public bool IsMiniature { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<ProductFile> ProductFiles { get; set; }

        public File()
        {
            ProductFiles = new HashSet<ProductFile>();
        }
    }
}