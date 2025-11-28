using AutoMapper;
using PostService.Application.UseCases.CreatePost;
using PostService.Domain;

namespace PostService.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, CreatePostResponse>();
            CreateMap<Media, CreatePostResponse_Media>();
        }
    }
}
