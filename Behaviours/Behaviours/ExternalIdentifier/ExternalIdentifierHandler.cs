using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Behaviours.Behaviours.ExternalIdentifier
{
    internal class ExternalIdentifierHandler : IOnSaveHandler
    {
        public void OnSave(ChangeTracker tracker)
        {
            var entries = tracker.Entries<IExternalIdentifierBehaviour>();
            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Added && !ExternalId.TryParse(entity.ExternalId, out _))
                {
                    entity.ExternalId = ExternalId.Create().ToString();
                }
            }
        }
    }
}
