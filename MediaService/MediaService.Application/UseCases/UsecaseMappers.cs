using AutoMapper;
using MediaService.Domain;
using Shared.Events.MediaService;

namespace MediaService.Application.UseCases
{
    internal class UsecaseMappers : Profile
    {
        public UsecaseMappers()
        {
            CreateMap<Media, MediaPreprocessingCompletedEvent>();
        }
    }
}
