using AutoMapper;
using PostService.Application.UseCases.CreatePost;
using PostService.Application.UseCases.CreatePostMedia;
using PostService.Application.UseCases.DeletePostMedia;
using PostService.Domain;
using Shared.Events.PostService;

namespace PostService.Api.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Content, CreatePostResponse_Content>();
            CreateMap<Media, CreatePostResponse_Media>();
            CreateMap<Post, CreatePostResponse>();

            CreateMap<CreatePostResponse_Content, PostCreatedEvent_Content>();
            CreateMap<CreatePostResponse_Media, PostCreatedEvent_Media>();
            CreateMap<CreatePostResponse, PostCreatedEvent>();

            CreateMap<DeletePostMediaResponse_Content, PostMediaDeletedEvent_Content>();
            CreateMap<DeletePostMediaResponse_Media, PostMediaDeletedEvent_Media>();
            CreateMap<DeletePostMediaResponse, PostMediaDeletedEvent>();

            CreateMap<CreatePostMediaResponse_Media, PostMediaCreatedEvent_Media>();
            CreateMap<CreatePostMediaResponse, PostMediaCreatedEvent>();
        }
    }
}
