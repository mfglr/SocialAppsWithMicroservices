using AutoMapper;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.SetPostContentModerationResult
{
    internal class SetPostContentModerationResultMapper : Profile
    {
        public SetPostContentModerationResultMapper()
        {
            CreateMap<PostContentModerationResultSetEvent_Content, UpdatePostRequest_Content>();
            CreateMap<PostContentModerationResultSetEvent_Media, UpdatePostRequest_Media>();
            CreateMap<PostContentModerationResultSetEvent, UpdatePostRequest>();
        }
    }
}
