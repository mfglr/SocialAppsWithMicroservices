using AutoMapper;
using PostService.Domain;

namespace PostService.Application.UseCases.DeletePostMedia
{
    internal class DeletePostMediaMapper : Profile
    {
        public DeletePostMediaMapper()
        {
            CreateMap<Content, DeletePostMediaResponse_Content>();
            CreateMap<Media, DeletePostMediaResponse_Media>();
            CreateMap<Post, DeletePostMediaResponse>();
        }
    }
}
