using AutoMapper;
using Shared.Events.UserService;
using UserService.Domain;

namespace UserService.Application.UseCases.CreateUser
{
    internal class CreateUserMapper : Profile
    {
        public CreateUserMapper()
        {
            CreateMap<ModerationResult, Shared.Events.ModerationResult>();
            CreateMap<Thumbnail, Shared.Events.Thumbnail>();
            CreateMap<Metadata, Shared.Events.Metadata>();
            CreateMap<Media, UserCreatedEvent_Media>();
            CreateMap<User, UserCreatedEvent>()
                .ForCtorParam("Username", cfg => cfg.MapFrom(x => x.Username.Value))
                .ForCtorParam("Name", cfg => cfg.MapFrom(x => x.Name.Value))
                .ForCtorParam("Gender", cfg => cfg.MapFrom(x => x.Gender.Value));
        }
    }
}
