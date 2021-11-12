using System;

namespace Ordering.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

    }
}
