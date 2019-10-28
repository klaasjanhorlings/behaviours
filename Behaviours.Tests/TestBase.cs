using Behaviours.Abstractions;
using Behaviours.Tests.Auxiliary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Behaviours.Tests
{
    [TestClass]
    public class TestBase
    {
        private static IServiceProvider RootServiceProvider;
        private IServiceScope ServiceScope;

        protected static TestDateTimeProvider DateTimeProvider = new TestDateTimeProvider();
        protected static TestOnSaveHandler OnSaveHandler = new TestOnSaveHandler();

        protected IServiceProvider ServiceProvider => ServiceScope?.ServiceProvider;
        protected TestDbContext Context;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            var sc = new ServiceCollection();
            sc.AddScoped<IDateTimeProvider>(sp => DateTimeProvider = new TestDateTimeProvider());
            sc.AddLogging(o => o.AddConsole());

            sc.AddBehaviours();
            sc.AddScoped<IOnSaveHandler>(sp => OnSaveHandler = new TestOnSaveHandler());
            sc.AddDbContext<TestDbContext>(cfg =>
            {
                cfg.UseSqlite("Data Source=\"test.db\"");
                cfg.UseLoggerFactory(LoggerFactory.Create(o => o.AddConsole()));
                cfg.EnableSensitiveDataLogging();
            });

            RootServiceProvider = sc.BuildServiceProvider();
            
            using (var ctx = RootServiceProvider.GetService<TestDbContext>())
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }
        }

        [TestInitialize]
        public void BaseInitialize()
        {
            ServiceScope = RootServiceProvider.CreateScope();
            Context = ServiceProvider.GetService<TestDbContext>();
        }

        [TestCleanup]
        public void BaseCleanup()
        {
            ServiceScope.Dispose();
            ServiceScope = null;
        }
    }
}
