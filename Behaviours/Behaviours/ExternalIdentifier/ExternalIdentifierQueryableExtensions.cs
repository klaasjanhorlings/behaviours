using Behaviours.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Behaviours.Behaviours.ExternalIdentifier
{
    public static class ExternalIdentifierQueryableExtensions
    {
        public static async Task<int> GetInternalIdAsync<TEntity>(this IQueryable<TEntity> dbSet, string id, CancellationToken cancellationToken = default) where TEntity : class, IExternalIdentifierBehaviour
        {
            var internalId = await dbSet
                .Where(x => x.ExternalId == id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (internalId == default) throw new EntityNotFoundException<TEntity>(id);

            return internalId;
        }
    }
}
