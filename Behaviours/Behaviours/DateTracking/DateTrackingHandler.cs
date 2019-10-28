using Behaviours.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Behaviours.Behaviours.DateTracking
{
    internal class DateTrackingHandler : IOnSaveHandler
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public DateTrackingHandler(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public void OnSave(ChangeTracker tracker)
        {
            var entries = tracker.Entries<IDateTrackingBehaviour>();
            var now = dateTimeProvider.Now;
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added && entry.Entity.CreatedAt == default)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Property(x => x.CreatedAt).IsModified = true;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedAt = now;
                    entry.Property(x => x.ModifiedAt).IsModified = true;
                }
            }
        }
    }
}
