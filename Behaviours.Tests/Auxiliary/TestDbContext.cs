using Behaviours.Data;
using Behaviours.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Behaviours.Tests.Auxiliary
{
    public class TestDbContext : AppDbContext
    {
        public TestDbContext(DbContextOptions options, IEnumerable<IOnSaveHandler> onSaveHandlers) : base(options, onSaveHandlers)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    ExternalId = ExternalId.Create(),
                    CreatedAt = DateTime.Now - TimeSpan.FromSeconds(1337),
                    Body = "First post!"
                },
                new Post
                {
                    Id = 2,
                    ExternalId = ExternalId.Create(),
                    CreatedAt = DateTime.Now - TimeSpan.FromSeconds(2400),
                    Body = "Second post!"
                }
            );
        }
    }
}
