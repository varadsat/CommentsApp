using CommentsApp.Models;
using Microsoft.AspNetCore.Identity;

namespace CommentsApp.DTOs
{
    public class CommentRequestDto
    {
        public string AssetId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
