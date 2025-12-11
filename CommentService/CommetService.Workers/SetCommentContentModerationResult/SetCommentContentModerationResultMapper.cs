using AutoMapper;
using CommentService.Application.UseCases.SetCommentContentModerationResult;
using Shared.Events.Comment;

namespace CommetService.Workers.SetCommentContentModerationResult
{
    internal class SetCommentContentModerationResultMapper : Profile
    {
        public SetCommentContentModerationResultMapper()
        {
            CreateMap<CommentContentClassifiedEvent, SetCommentContentModerationResultRequest>();
            CreateMap<SetCommentContentModerationResultResponse_Content, CommentContentModerationResultSetEvent_Content>();
            CreateMap<SetCommentContentModerationResultResponse, CommentContentModerationResultSetEvent>();
        }
    }
}
