using Microsoft.AspNetCore.Identity;

namespace CommentsApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string AssetId { get; set; } = string.Empty; 
        public Asset Asset { get; set; }
        public string UserId { get; set; } = string.Empty;
        public IdentityUser IdentityUser { get; set; }
        public ICollection<Comment>? ChildComments { get; set; }
        public string Body { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }
}
