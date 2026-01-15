using AutoMapper;
using PostService.Domain;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.UpdatePostContent
{
    internal class UpdatePostContentMapper : Profile
    {
        public UpdatePostContentMapper()
        {
            CreateMap<Content, PostContentUpdatedEvent_Content>();
            CreateMap<Post, PostContentUpdatedEvent>();
        }
    }
}
