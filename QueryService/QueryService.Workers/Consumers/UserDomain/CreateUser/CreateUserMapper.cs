using AutoMapper;
using QueryService.Application.UseCases.UserUseCases.UpdateUser;
using Shared.Events.UserService;

namespace QueryService.Workers.Consumers.UserDomain.CreateUser
{
    internal class CreateUserMapper : Profile
    {
        public CreateUserMapper()
        {
            CreateMap<UserCreatedEvent, UpdateUserRequest>();
        }
    }
}
