using AutoMapper;
using UserService.Domain;

namespace UserService.Application.UseCases.GetUserById
{
    internal class GetUserByIdMapper : Profile
    {
        public GetUserByIdMapper()
        {
            CreateMap<ModerationResult, Shared.Events.ModerationResult>();
            CreateMap<Thumbnail, Shared.Events.Thumbnail>();
            CreateMap<Metadata, Shared.Events.Metadata>();
            CreateMap<Media, GetUserByIdResponse_Media>();
            CreateMap<User, GetUserByIdResponse>()
                .ForCtorParam("Username", cfg => cfg.MapFrom(x => x.Username.Value))
                .ForCtorParam("Name", cfg => cfg.MapFrom(x => x.Name.Value))
                .ForCtorParam("Gender", cfg => cfg.MapFrom(x => x.Gender.Value));
        }
    }
}
