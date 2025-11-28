using AutoMapper;
using MediaService.Application.UseCases;
using MediaService.Application.UseCases.SetMediaMetadata;
using MediaService.Application.UseCases.SetMediaModerationResult;
using MediaService.Application.UseCases.SetMediaThumbnail;
using MediaService.Application.UseCases.SetMediaTranscodedBlobName;
using MediaService.Domain;
using Shared.Events.Media;

namespace MediaService.Workers.Mappers
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Media, MediaResponse>();
            CreateMap<MediaResponse, MediaPreprocessingCompletedEvent>();
        }
    }
}
