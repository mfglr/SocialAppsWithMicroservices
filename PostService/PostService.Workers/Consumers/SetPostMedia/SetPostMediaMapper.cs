using AutoMapper;
using PostService.Application.UseCases.SetPostMedia;
using Shared.Events.Media;
using Shared.Events.PostService;

namespace PostService.Workers.Consumers.SetPostMedia
{
    internal class SetPostMediaMapper : Profile
    {
        public SetPostMediaMapper()
        {
            CreateMap<MediaPreprocessingCompletedEvent, SetPostMediaRequest>();
            CreateMap<SetPostMediaResponse_Content, PostMediaSetEvent_Content>();
            CreateMap<SetPostMediaResponse_Media, PostMediaSetEvent_Media>();
            CreateMap<SetPostMediaResponse, PostMediaSetEvent>();
        }
    }
}
