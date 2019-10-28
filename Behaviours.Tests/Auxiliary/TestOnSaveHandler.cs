using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Behaviours.Tests.Auxiliary
{
    public class TestOnSaveHandler : IOnSaveHandler
    {
        public Action<ChangeTracker> Handler { get; set; }
        public bool IsCalled { get; private set; }

        public void OnSave(ChangeTracker tracker)
        {
            IsCalled = true;
            Handler?.Invoke(tracker);
        }
    }
}
