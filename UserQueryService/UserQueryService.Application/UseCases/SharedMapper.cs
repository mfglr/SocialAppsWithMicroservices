using AutoMapper;
using UserQueryService.Domain;

namespace UserQueryService.Application.UseCases
{
    internal class SharedMapper : Profile
    {
        public SharedMapper()
        {
            CreateMap<Media, UserResponse_Media>();
            CreateMap<User, UserResponse>();
        }
    }
}
