using AutoMapper;

namespace PostMedia.Application.UseCases.AddPostMediaThumbnail
{
    internal class AddPostMediaThumbnailMapper : Profile
    {
        public AddPostMediaThumbnailMapper()
        {
            CreateMap<Shared.Events.Thumbnail, Domain.Thumbnail>();
        }
    }
}
