using System;

namespace Behaviours.Behaviours.DateTracking
{
    public interface IDateTrackingBehaviour
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
