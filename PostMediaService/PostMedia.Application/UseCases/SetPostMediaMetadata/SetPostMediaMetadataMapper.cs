using AutoMapper;

namespace PostMedia.Application.UseCases.SetPostMediaMetadata
{
    internal class SetPostMediaMetadataMapper : Profile
    {
        public SetPostMediaMetadataMapper()
        {
            CreateMap<Shared.Events.Metadata, Domain.Metadata>();
        }
    }
}
