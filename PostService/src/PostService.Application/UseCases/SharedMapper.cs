using AutoMapper;

namespace PostService.Application.UseCases
{
    internal class SharedMapper : Profile
    {
        public SharedMapper()
        {
            CreateMap<Shared.Events.Metadata, Domain.Metadata>().ReverseMap();
            CreateMap<Shared.Events.ModerationResult, Domain.ModerationResult>().ReverseMap();
            CreateMap<Shared.Events.Thumbnail, Domain.Thumbnail>().ReverseMap();
        }
    }
}
