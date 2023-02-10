using CommentsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace CommentsApp.Data
{
    public class CommentsContext :DbContext
    {
        public CommentsContext(DbContextOptions<CommentsContext> options) : base(options) { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Asset> Assets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
    
}
