using AutoMapper;
using UserQueryService.Domain;

namespace UserQueryService.Application.UseCases.UpsertUser
{
    internal class UpsertUserMapper : Profile
    {
        public UpsertUserMapper()
        {
            CreateMap<UpsertUserRequest_Media, Media>();
        }
    }
}
