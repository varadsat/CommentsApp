using CommentsApp.DTOs;
using CommentsApp.Models;

namespace CommentsApp.Services.CommentServices
{
    public interface ICommentService
    {
        
        Task<Comment> CreateComment(CommentRequestDto requestDto);
        Task<Comment> CreateChildComment(CommentRequestDto requestDto, int parentId);
        Task<List<Comment>> GetAllComments();
        Task<List<Comment>> GetChildComments(int id);
        Task DeleteComment(int id);
    }
}
