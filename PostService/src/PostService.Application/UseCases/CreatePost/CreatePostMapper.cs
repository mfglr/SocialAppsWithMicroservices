using AutoMapper;
using PostService.Domain;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.CreatePost
{
    internal class CreatePostMapper : Profile
    {
        public CreatePostMapper()
        {
            CreateMap<Content, PostCreatedEvent_Content>();
            CreateMap<Media, PostCreatedEvent_Media>();
            CreateMap<Post, PostCreatedEvent>();
        }
    }
}
