using AutoMapper;
using PostService.Domain;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.RestorePost
{
    internal class RestorePostMapper : Profile
    {
        public RestorePostMapper()
        {
            CreateMap<Content, PostRestoredEvent_Content>();
            CreateMap<Media, PostRestoredEvent_Media>();
            CreateMap<Post, PostRestoredEvent>();
        }
    }
}
