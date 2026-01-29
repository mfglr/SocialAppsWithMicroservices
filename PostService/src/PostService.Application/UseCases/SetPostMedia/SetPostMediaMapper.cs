using AutoMapper;
using PostService.Domain;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.SetPostMedia
{
    internal class SetPostMediaMapper : Profile
    {
        public SetPostMediaMapper()
        {
            CreateMap<SetPostMediaRequest_Media, Media>();

            CreateMap<Content, PostPreproccessingCompletedEvent_Content>();
            CreateMap<Media, PostPreproccessingCompletedEvent_Media>();
            CreateMap<Post, PostPreproccessingCompletedEvent>();
        }
    }
}
