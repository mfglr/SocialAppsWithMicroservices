using AutoMapper;
using Shared.Events;

namespace UserService.Application.UseCases.AddMediaThumbnail
{
    internal class AddMediaThumbnailMapper : Profile
    {
        public AddMediaThumbnailMapper()
        {
            CreateMap<Thumbnail, Domain.Thumbnail>();
        }
    }
}
