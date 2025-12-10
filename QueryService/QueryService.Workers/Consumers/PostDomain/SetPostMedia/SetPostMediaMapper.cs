using AutoMapper;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.SetPostMedia
{
    internal class SetPostMediaMapper : Profile
    {
        public SetPostMediaMapper()
        {
            CreateMap<PostMediaSetEvent_Content, UpdatePostRequest_Content>();
            CreateMap<PostMediaSetEvent_Media, UpdatePostRequest_Media>();
            CreateMap<PostMediaSetEvent, UpdatePostRequest>();
        }
    }
}
