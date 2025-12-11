using AutoMapper;
using CommentService.Domain;

namespace CommentService.Application.UseCases.CreateComment
{
    internal class CreateCommentMapper : Profile
    {
        public CreateCommentMapper()
        {
            CreateMap<Content, CreateCommentResponse_Content>();
            CreateMap<Comment, CreateCommentResponse>();
        }
    }
}
