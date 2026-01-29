using AutoMapper;
using PostMedia.Domain;
using Shared.Events.PostMediaService;

namespace PostMedia.Application.UseCases
{
    internal class SharedMapper : Profile
    {
        public SharedMapper()
        {
            CreateMap<Metadata, Shared.Events.Metadata>();
            CreateMap<ModerationResult, Shared.Events.ModerationResult>();
            CreateMap<Thumbnail, Shared.Events.Thumbnail>();
            CreateMap<Media, PostMediaPreproccessingCompletedEvent_Media>();
            CreateMap<Post, PostMediaPreproccessingCompletedEvent>();
        }
    }
}
