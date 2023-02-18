using AutoMapper;
using CommentsApp.Data;
using CommentsApp.DTOs;
using CommentsApp.Extensions;
using CommentsApp.Models;
using CommentsApp.Services.AssetServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace CommentsApp.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly CommentsContext _context;
        private readonly IMapper _mapper;
        private readonly IAssetService _assetService;

        public CommentService(CommentsContext commentsContext, IMapper mapper, IAssetService assetService)
        {
            _context = commentsContext;
            _mapper = mapper;
            _assetService = assetService;
        }



        public async Task<Comment> CreateChildComment(CommentRequestDto requestDto, int parentId)
        {
            var childComment = requestDto.ToModel();
            var parentComment = await _context.Comments.FindAsync(parentId);
            if (parentComment == null)
                return null;
            parentComment!.ChildComments!.Add(childComment);
            await _context.SaveChangesAsync();
            return childComment;
        }

        public async Task<Comment> CreateComment(CommentRequestDto requestDto)
        {
            var asset = await _context.Assets.FindAsync(requestDto.AssetId) ?? await _assetService.CreateAsset(requestDto.AssetId);
            var comment = requestDto.ToModel();
            comment.ChildComments = new List<Comment>();
            await _context.Comments.AddAsync(comment);
            asset.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return;
            foreach (var child in comment.ChildComments)
            {
                _context.Comments.Remove(child);
            }
            //_context.DeleteComment(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAllComments()
        {
            var comments = await _context.Comments.ToListAsync();
            return comments;
        }

        public async Task<List<Comment>> GetChildComments(int id)
        {
            var comment = await _context.Comments.Include(comment => comment.ChildComments).FirstOrDefaultAsync(c => c.Id == id);
            return comment.ChildComments.ToList();
        }
    }
}
