using AutoMapper;
using MediaService.Domain;
using Shared.Events.MediaService;

namespace MediaService.Application.UseCases.CreateMedia
{
    internal class CreateMediaMapper : Profile
    {
        public CreateMediaMapper()
        {
            CreateMap<Media, MediaCreatedEvent>();
        }
    }
}
