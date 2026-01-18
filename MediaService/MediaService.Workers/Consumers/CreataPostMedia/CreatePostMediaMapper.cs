using AutoMapper;
using MediaService.Application.UseCases.CreateMedia;
using Shared.Events.PostService;

namespace MediaService.Workers.Consumers.CreataPostMedia
{
    internal class CreatePostMediaMapper : Profile
    {
        public CreatePostMediaMapper()
        {
            CreateMap<PostMediaCreatedEvent, CreateMediaRequest>();
        }
    }
}
