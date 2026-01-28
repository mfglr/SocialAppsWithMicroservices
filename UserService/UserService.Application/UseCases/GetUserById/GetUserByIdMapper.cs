using AutoMapper;
using UserService.Domain;

namespace UserService.Application.UseCases.GetUserById
{
    internal class GetUserByIdMapper : Profile
    {
        public GetUserByIdMapper()
        {
            CreateMap<ModerationResult, Shared.Objects.ModerationResult>();
            CreateMap<Thumbnail, Shared.Objects.Thumbnail>();
            CreateMap<Metadata, Shared.Objects.Metadata>();
            CreateMap<Media, Shared.Objects.Media>();
            CreateMap<User, GetUserByIdResponse>()
                .ForCtorParam("Username", cfg => cfg.MapFrom(x => x.Username.Value))
                .ForCtorParam("Name", cfg => cfg.MapFrom(x => x.Name.Value))
                .ForCtorParam("Gender", cfg => cfg.MapFrom(x => x.Gender.Value));
        }
    }
}
