using AutoMapper;
using UserService.Domain;

namespace UserService.Application.UseCases.SetMediaMetadata
{
    internal class SetMediaMetadataMapper : Profile
    {
        public SetMediaMetadataMapper() 
        {
            CreateMap<Shared.Events.Metadata, Metadata>();
        }
    }
}
