using AutoMapper;
using PostService.Domain;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.DeletePost
{
    internal class DeletePostMapper : Profile
    {
        public DeletePostMapper()
        {
            CreateMap<Content, PostDeletedEvent_Content>();
            CreateMap<Post, PostDeletedEvent>();
        }
    }
}
