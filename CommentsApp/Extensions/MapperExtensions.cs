using CommentsApp.DTOs;
using CommentsApp.Models;

namespace CommentsApp.Extensions
{
    public static class MapperExtensions
    {
        public static Comment ToModel(this CommentRequestDto requestDto)
        {
            return new Comment
            {
                AssetId = requestDto.AssetId,
                UserId = requestDto.UserId,
                Body = requestDto.Body
            };
        }
    }

}
