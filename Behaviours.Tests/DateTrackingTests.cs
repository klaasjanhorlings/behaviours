using Behaviours.Behaviours.DateTracking;
using Behaviours.Behaviours.ExternalIdentifier;
using Behaviours.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Behaviours.Tests
{
    [TestClass]
    public class DateTrackingTests : TestBase
    {
        [TestMethod]
        public void NewDateTrackingEntitiesGetCreatedAtValue()
        {
            var now = DateTime.Now;
            DateTimeProvider.Now = now;
            OnSaveHandler.Handler = ct =>
            {
                var entry = ct.Entries<IDateTrackingBehaviour>().First();

                Assert.AreEqual(EntityState.Added, entry.State);
                Assert.AreEqual(now, entry.Entity.CreatedAt);

                Assert.IsNull(entry.Entity.ModifiedAt);
            };

            Context.Add(new Post { Body = "Added post" });
            Context.SaveChanges();

            Assert.IsTrue(OnSaveHandler.IsCalled, "OnSaveHandler has not been called");
        }

        [TestMethod]
        public void UpdatedDateTrackingEntitesGetModifiedAtValue()
        {
            var now = DateTime.Now;
            DateTimeProvider.Now = now;
            OnSaveHandler.Handler = ct =>
            {
                var entry = ct.Entries<IDateTrackingBehaviour>().First();

                Assert.AreEqual(EntityState.Modified, entry.State);
                Assert.AreEqual(now, entry.Entity.ModifiedAt);

                Assert.IsFalse(entry.Property(x => x.CreatedAt).IsModified, "CreatedAt should not be modified");
            };

            var post = Context.Posts.First();
            post.Body = "New Body";

            Context.SaveChanges();

            Assert.IsTrue(OnSaveHandler.IsCalled, "OnSaveHandler has not been called");
        }
    }
}
