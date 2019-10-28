using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Behaviours
{
    public class BehaviourDbContext : DbContext
    {
        public readonly IList<IOnSaveHandler> OnSaveHandlers;

        public BehaviourDbContext(DbContextOptions options, IEnumerable<IOnSaveHandler> onSaveHandlers) : base(options)
        {
            OnSaveHandlers = onSaveHandlers == null
                ? new List<IOnSaveHandler>() 
                : new List<IOnSaveHandler>(onSaveHandlers);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            RunHandlers();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            RunHandlers();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected void RunHandlers()
        {
            foreach (var handler in OnSaveHandlers)
            {
                handler.OnSave(ChangeTracker);
            }
        }
    }
}
