using System;

namespace Behaviours.Behaviours.SoftDelete
{
    public interface ISoftDeleteBehaviour
    {
        public DateTime? DeletedAt { get; set; }
    }
}
