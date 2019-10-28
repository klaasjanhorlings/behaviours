using Behaviours.Behaviours.ExternalIdentifier;
using Behaviours.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Behaviours.Tests
{
    [TestClass]
    public class ExternalIdentifierTests: TestBase
    {
        [TestMethod]
        public void CreatesNewExternalIdentifierForNewEntities()
        {
            OnSaveHandler.Handler = ct =>
            {
                var entry = ct.Entries<IExternalIdentifierBehaviour>().First();

                Assert.IsNotNull(entry.Entity.ExternalId, $"Expecting {entry.Entity.ExternalId} to be not null");
                Assert.IsTrue(ExternalId.TryParse(entry.Entity.ExternalId, out var _), $"Expecting {nameof(entry.Entity.ExternalId)} to be a valid ExternalId");
            };

            var post = new Post { Body = "ExternalId post" };
            Context.Posts.Add(post);
            Context.SaveChanges();

            Assert.IsTrue(OnSaveHandler.IsCalled, "OnSaveHandler has not been called");
        }

        [TestMethod]
        public void DoesNotOverrideSetExternalId()
        {
            var expectedId = "Abbreviation";
            OnSaveHandler.Handler = ct =>
            {
                var entry = ct.Entries<IExternalIdentifierBehaviour>().First();

                Assert.AreEqual(expectedId, entry.Entity.ExternalId);
            };

            var post = new Post { Body = "Does not override external id", ExternalId = expectedId };
            Context.Posts.Add(post);
            Context.SaveChanges();

            Assert.IsTrue(OnSaveHandler.IsCalled, "OnSaveHandler has not been called");
        }

        [TestMethod]
        public async Task GetInternalIdReturnsExpectedId()
        {
            var post = Context.Posts.AsNoTracking().First();
            var id = await Context.Posts.GetInternalIdAsync(post.ExternalId);

            Assert.AreEqual(post.Id, id);
        }
    }
}
