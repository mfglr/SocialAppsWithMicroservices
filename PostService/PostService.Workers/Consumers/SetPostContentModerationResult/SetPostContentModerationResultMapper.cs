using AutoMapper;
using PostService.Application.UseCases.SetPostContentModerationResult;
using Shared.Events.PostService;

namespace PostService.Workers.Consumers.SetPostContentModerationResult
{
    internal class SetPostContentModerationResultMapper : Profile
    {
        public SetPostContentModerationResultMapper()
        {
            CreateMap<SetPostContentModerationResultResponse_Content, PostContentModerationResultSetEvent_Content>();
            CreateMap<SetPostContentModerationResultResponse_Media, PostContentModerationResultSetEvent_Media>();
            CreateMap<SetPostContentModerationResultResponse, PostContentModerationResultSetEvent>();
        }
    }
}
