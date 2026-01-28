using AutoMapper;

namespace UserService.Application.UseCases.AddMediaThumbnail
{
    internal class AddMediaThumbnailMapper : Profile
    {
        public AddMediaThumbnailMapper()
        {
            CreateMap<Shared.Objects.Thumbnail, Domain.Thumbnail>();
        }
    }
}
