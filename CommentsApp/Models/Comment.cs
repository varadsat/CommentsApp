using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace CommentsApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? AssetId { get; set; }

        public Asset? Asset { get; set; } 
        public string UserId { get; set; } = default!;
        public IdentityUser User { get; set; } = default!;
        public ICollection<Comment> ChildComments { get; set; } = new HashSet<Comment>();
        public int? ParentCommentId { get; set; }
        [JsonIgnore]
        public Comment? ParentComment { get; set; }
        public string Body { get; set; } = default!;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
    
}
