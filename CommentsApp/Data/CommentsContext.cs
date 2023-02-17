using CommentsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Xml;

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
        public void DeleteComment(Comment childComment)
        {
            var target = Comments
                .Include(x => x.ChildComments)
                .FirstOrDefault(x => x.Id == childComment.Id);  

            RecursiveDelete(target);

            SaveChanges();
        }

        private void RecursiveDelete(Comment parent)
        {
            if (parent.ChildComments != null)
            {
                var children = Comments
                    .Include(x => x.ChildComments)
                    .Where(x => parent.ChildComments.Contains(x));

                foreach (var child in children)
                {
                    RecursiveDelete(child); 
                }
            }

            Comments.Remove(parent);
        }
    }
    
}
