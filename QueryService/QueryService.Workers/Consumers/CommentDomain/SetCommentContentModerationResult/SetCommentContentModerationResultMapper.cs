using AutoMapper;
using QueryService.Application.UseCases.CommentUseCases.UpdateComent;
using Shared.Events.Comment;

namespace QueryService.Workers.Consumers.CommentDomain.SetCommentContentModerationResult
{
    internal class SetCommentContentModerationResultMapper : Profile
    {
        public SetCommentContentModerationResultMapper()
        {
            CreateMap<CommentContentModerationResultSetEvent_Content, UpdateCommentRequest_Content>();
            CreateMap<CommentContentModerationResultSetEvent, UpdateCommentRequest>();
        }
    }
}
