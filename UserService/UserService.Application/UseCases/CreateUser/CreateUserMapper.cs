using AutoMapper;
using Shared.Events.UserService;
using UserService.Domain;

namespace UserService.Application.UseCases.CreateUser
{
    internal class CreateUserMapper : Profile
    {
        public CreateUserMapper()
        {
            CreateMap<ModerationResult, Shared.Objects.ModerationResult>();
            CreateMap<Thumbnail, Shared.Objects.Thumbnail>();
            CreateMap<Metadata, Shared.Objects.Metadata>();
            CreateMap<Media, Shared.Objects.Media>();
            CreateMap<User, UserCreatedEvent>()
                .ForCtorParam("Username", cfg => cfg.MapFrom(x => x.Username.Value))
                .ForCtorParam("Name", cfg => cfg.MapFrom(x => x.Name.Value))
                .ForCtorParam("Gender", cfg => cfg.MapFrom(x => x.Gender.Value));
        }
    }
}
