using CommentsApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Xml;

namespace CommentsApp.Data
{
    public class CommentsContext :IdentityDbContext<IdentityUser>
    {
        public CommentsContext(DbContextOptions<CommentsContext> options) : base(options) { }
        //public CommentsContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Asset> Assets => Set<Asset>();
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            
            modelBuilder.Entity<Asset>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Asset)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>()
                .HasMany(c => c.ChildComments)
                .WithOne(c => c.ParentComment)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
    
}
