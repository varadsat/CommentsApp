namespace CommentsApp.Models
{
    public class Asset
    {
        public string Id { get; set; } = string.Empty;
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
    }
}
