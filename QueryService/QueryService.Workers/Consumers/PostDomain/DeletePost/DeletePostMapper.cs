using AutoMapper;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.DeletePost
{
    internal class DeletePostMapper : Profile
    {
        public DeletePostMapper()
        {
            CreateMap<PostDeletedEvent_Content, UpdatePostRequest_Content>();
            CreateMap<PostDeletedEvent_Media, UpdatePostRequest_Media>();
            CreateMap<PostDeletedEvent, UpdatePostRequest>();
        }
    }
}
