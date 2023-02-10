using AutoMapper;
using CommentsApp.DTOs;
using CommentsApp.Models;

namespace CommentsApp.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CommentRequestDto, Comment>();
        }
    }
}
