using AutoMapper;
using MassTransit;
using MediatR;
using PostMedia.Application.UseCases.CreatePost;
using Shared.Events.PostService;

namespace PostMedia.Worker.Consumers
{
    internal class CreatePostMedia_OnPostCreated_Mapper : Profile
    {
        public CreatePostMedia_OnPostCreated_Mapper()
        {
            CreateMap<PostCreatedEvent_Media, CreatePostRequest_Media>();
            CreateMap<PostCreatedEvent, CreatePostRequest>();
        }
    }

    internal class CreatePostMedia_OnPostCreated_PostMediService(ISender sender, IMapper mapper) : IConsumer<PostCreatedEvent>
    {
        public Task Consume(ConsumeContext<PostCreatedEvent> context) =>
            sender
                .Send(
                    mapper.Map<PostCreatedEvent, CreatePostRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
