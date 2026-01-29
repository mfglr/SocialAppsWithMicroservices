using AutoMapper;
using PostService.Domain;

namespace PostService.Application.UseCases.GetPostById
{
    internal class GetPostByIdMapper : Profile
    {
        public GetPostByIdMapper()
        {
            CreateMap<Content, GetPostByIdResponse_Content>();
            CreateMap<Media, GetPostByIdResponse_Media>();
            CreateMap<Post, GetPostByIdResponse>();
        }
    }
}
