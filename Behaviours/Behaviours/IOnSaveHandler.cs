using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Behaviours
{
    public interface IOnSaveHandler
    {
        void OnSave(ChangeTracker tracker);
    }
}
