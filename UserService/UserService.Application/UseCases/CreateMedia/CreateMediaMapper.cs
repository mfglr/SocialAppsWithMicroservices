using AutoMapper;
using UserService.Domain;

namespace UserService.Application.UseCases.CreateMedia
{
    internal class CreateMediaMapper : Profile
    {
        public CreateMediaMapper()
        {
            CreateMap<Media, Shared.Objects.Media>();
        }
    }
}
