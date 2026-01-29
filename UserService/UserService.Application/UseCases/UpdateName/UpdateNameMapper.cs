using AutoMapper;
using Shared.Events.UserService;
using UserService.Domain;

namespace UserService.Application.UseCases.UpdateName
{
    internal class UpdateNameMapper : Profile
    {
        public UpdateNameMapper()
        {
            CreateMap<ModerationResult, Shared.Events.ModerationResult>();
            CreateMap<Thumbnail, Shared.Events.Thumbnail>();
            CreateMap<Metadata, Shared.Events.Metadata>();
            CreateMap<Media, NameUpdatedEvent_Media>();
            CreateMap<User, NameUpdatedEvent>()
                .ForCtorParam("Username", cfg => cfg.MapFrom(x => x.Username.Value))
                .ForCtorParam("Name", cfg => cfg.MapFrom(x => x.Name.Value))
                .ForCtorParam("Gender", cfg => cfg.MapFrom(x => x.Gender.Value));
        }
    }
}
