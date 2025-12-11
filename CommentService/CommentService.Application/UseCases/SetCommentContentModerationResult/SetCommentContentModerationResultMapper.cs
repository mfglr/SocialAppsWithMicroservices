using AutoMapper;
using CommentService.Domain;

namespace CommentService.Application.UseCases.SetCommentContentModerationResult
{
    internal class SetCommentContentModerationResultMapper : Profile
    {
        public SetCommentContentModerationResultMapper()
        {
            CreateMap<Content, SetCommentContentModerationResultResponse_Content>();
            CreateMap<Comment, SetCommentContentModerationResultResponse>();
        }
    }
}
