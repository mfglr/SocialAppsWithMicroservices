using AutoMapper;
using CommentService.Domain;

namespace CommentService.Application.UseCases.RestorePostComments
{
    internal class RestorePostCommentsMapper : Profile
    {
        public RestorePostCommentsMapper()
        {
            CreateMap<Content, RestorePostCommentsResponse_Content>();
            CreateMap<Comment, RestorePostCommentsResponse_Comment>();
        }
    }
}
