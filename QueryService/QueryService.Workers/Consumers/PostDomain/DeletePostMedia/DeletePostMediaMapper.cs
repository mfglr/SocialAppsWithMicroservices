using AutoMapper;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.DeletePostMedia
{
    internal class DeletePostMediaMapper : Profile
    {
        public DeletePostMediaMapper()
        {
            CreateMap<PostMediaDeletedEvent_Content, UpdatePostRequest_Content>();
            CreateMap<PostMediaDeletedEvent_Media, UpdatePostRequest_Media>();
            CreateMap<PostMediaDeletedEvent, UpdatePostRequest>();
        }
    }
}
