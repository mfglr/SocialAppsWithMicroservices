using AutoMapper;
using UserQueryService.Domain;

namespace UserQueryService.Application.UseCases
{
    internal class UseCaseMapper : Profile
    {
        public UseCaseMapper()
        {
            CreateMap<User, UserResponse>();
        }
    }
}
