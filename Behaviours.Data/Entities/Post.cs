using Behaviours.Behaviours.DateTracking;
using Behaviours.Behaviours.ExternalIdentifier;
using Behaviours.Behaviours.SoftDelete;
using System;
using System.ComponentModel.DataAnnotations;

namespace Behaviours.Data.Entities
{
    public class Post : IDateTrackingBehaviour, IExternalIdentifierBehaviour, ISoftDeleteBehaviour
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ExternalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public string Body { get; set; }
    }
}
