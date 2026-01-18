using AutoMapper;
using MediaService.Application.UseCases.CreateMedia;
using Shared.Events.PostService;

namespace MediaService.Workers.Consumers.CreateMedia
{
    public class CreateMediaMapper : Profile
    {
        public CreateMediaMapper()
        {
            CreateMap<PostCreatedEvent, CreateMediaRequest>();
        }

    }
}
