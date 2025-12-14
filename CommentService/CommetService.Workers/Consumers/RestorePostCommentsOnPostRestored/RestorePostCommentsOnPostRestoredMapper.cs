using AutoMapper;
using CommentService.Application.UseCases.RestorePostComments;
using Shared.Events.Comment;

namespace CommetService.Workers.Consumers.RestorePostCommentsOnPostRestored
{
    internal class RestorePostCommentsOnPostRestoredMapper : Profile
    {
        public RestorePostCommentsOnPostRestoredMapper()
        {
            CreateMap<RestorePostCommentsResponse_Content, CommentRestoredEvent_Content>();
            CreateMap<RestorePostCommentsResponse_Comment, CommentRestoredEvent>();
        }
    }
}
