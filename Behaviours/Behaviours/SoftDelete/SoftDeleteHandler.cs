using Behaviours.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Behaviours.Behaviours.SoftDelete
{
    internal class SoftDeleteHandler : IOnSaveHandler
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public SoftDeleteHandler(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public void OnSave(ChangeTracker tracker)
        {
            var entries = tracker.Entries<ISoftDeleteBehaviour>();
            var now = dateTimeProvider.Now;
            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Unchanged;
                    entry.Property(x => x.DeletedAt).IsModified = true;
                    entity.DeletedAt = dateTimeProvider.Now;
                }
            }
        }
    }
}
