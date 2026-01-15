using AutoMapper;
using PostService.Domain;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.CreatePostMedia
{
    internal class CreatePostMediaMapper : Profile
    {
        public CreatePostMediaMapper()
        {
            CreateMap<Content, PostMediaCreatedEvent_Content>();
            CreateMap<Post, PostMediaCreatedEvent>();
        }
    }
}
