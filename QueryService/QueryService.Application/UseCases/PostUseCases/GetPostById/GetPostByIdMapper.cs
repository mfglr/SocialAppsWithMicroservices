using AutoMapper;
using QueryService.Domain.PostDomain;

namespace QueryService.Application.UseCases.PostUseCases.GetPostById
{
    internal class GetPostByIdMapper : Profile
    {
        public GetPostByIdMapper()
        {
            CreateMap<PostContent, GetPostByIdResponse_Content>();
            CreateMap<Media, GetPostByIdResponse_Media>();
            CreateMap<Post, GetPostByIdResponse>();
        }
    }
}
