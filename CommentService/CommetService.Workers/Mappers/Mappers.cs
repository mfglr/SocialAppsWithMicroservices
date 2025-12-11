using AutoMapper;
using CommentService.Application.UseCases.CreateComment;
using Shared.Events.Comment;

namespace CommetService.Workers.Mappers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<CreateCommentResponse_Content, CommentCreatedEvent_Content>();
            CreateMap<CreateCommentResponse, CommentCreatedEvent>();
        }
    }
}
