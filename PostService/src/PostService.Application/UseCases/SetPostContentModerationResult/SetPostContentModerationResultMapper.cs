using AutoMapper;
using PostService.Domain;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.SetPostContentModerationResult
{
    internal class SetPostContentModerationResultMapper : Profile
    {
        public SetPostContentModerationResultMapper()
        {
            CreateMap<Content, PostContentModerationResultSetEvent_Content>();
            CreateMap<Media, PostContentModerationResultSetEvent_Media>();
            CreateMap<Post, PostContentModerationResultSetEvent>();
        }
    }
}
