using AutoMapper;
using PostMedia.Domain;

namespace PostMedia.Application.UseCases.CreatePost
{
    internal class CreatePostMapper : Profile
    {
        public CreatePostMapper()
        {
            CreateMap<CreatePostRequest_Media, Media>();
        }
    }
}
