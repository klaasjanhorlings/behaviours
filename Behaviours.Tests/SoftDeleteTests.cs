using Behaviours.Behaviours.SoftDelete;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Behaviours.Tests
{
    [TestClass]
    public class SoftDeleteTests : TestBase
    {
        [TestMethod]
        public void SetsDeletedAtWhenDeletingAnEntity()
        {
            var now = DateTime.Now;
            DateTimeProvider.Now = now;
            OnSaveHandler.Handler = ct =>
            {
                var entry = ct.Entries<ISoftDeleteBehaviour>().First();

                Assert.AreEqual(EntityState.Modified, entry.State);
                Assert.IsTrue(entry.Property(x => x.DeletedAt).IsModified);
                Assert.AreEqual(now, entry.Entity.DeletedAt);
            };

            var post = Context.Posts.Find(2);
            Context.Remove(post);
            Context.SaveChanges();

            Assert.IsTrue(OnSaveHandler.IsCalled, "OnSaveHandler has not been called");
        }
    }
}
