using Behaviours.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Behaviours.Data
{
    public class AppDbContext : BehaviourDbContext
    {
        public DbSet<Post> Posts { get; set; }

        public AppDbContext(DbContextOptions options, IEnumerable<IOnSaveHandler> onSaveHandlers) : base(options, onSaveHandlers)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*var post = modelBuilder.Entity<Post>();
            post.HasKey(x => x.Id);
            post.Property(x => x.Id).ValueGeneratedOnAdd();*/
        }
    }
}
