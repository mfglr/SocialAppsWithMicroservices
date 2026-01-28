using AutoMapper;
using Shared.Events.UserService;
using UserService.Domain;

namespace UserService.Application.UseCases
{
    internal class SharedMapper : Profile
    {
        public SharedMapper()
        {
            CreateMap<User, UserUpdatedEvent>();
        }
    }
}
