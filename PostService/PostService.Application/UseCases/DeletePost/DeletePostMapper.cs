using AutoMapper;
using PostService.Domain;

namespace PostService.Application.UseCases.DeletePost
{
    internal class DeletePostMapper : Profile
    {
        public DeletePostMapper()
        {
            CreateMap<Content, DeletePostResponse_Content>();
            CreateMap<Media, DeletePostResponse_Media>();
            CreateMap<Post, DeletePostResponse>();
        }
    }
}
