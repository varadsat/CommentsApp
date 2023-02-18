namespace CommentsApp.Models
{
    public class Asset
    {
        public string Id { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
