using Behaviours.Behaviours.DateTracking;
using Behaviours.Behaviours.ExternalIdentifier;
using Behaviours.Behaviours.SoftDelete;
using Microsoft.Extensions.DependencyInjection;

namespace Behaviours
{
    public static class ServiceExtensions
    {
        public static void AddBehaviours(this IServiceCollection services)
        {
            services.AddScoped<IOnSaveHandler, DateTrackingHandler>();
            services.AddScoped<IOnSaveHandler, ExternalIdentifierHandler>();
            services.AddScoped<IOnSaveHandler, SoftDeleteHandler>();
        }
    }
}
