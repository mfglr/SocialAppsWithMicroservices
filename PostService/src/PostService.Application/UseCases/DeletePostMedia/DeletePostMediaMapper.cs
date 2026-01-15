using AutoMapper;
using PostService.Domain;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.DeletePostMedia
{
    internal class DeletePostMediaMapper : Profile
    {
        public DeletePostMediaMapper()
        {
            CreateMap<Content, PostMediaDeletedEvent_Content>();
            CreateMap<Post, PostMediaDeletedEvent>();
        }
    }
}
