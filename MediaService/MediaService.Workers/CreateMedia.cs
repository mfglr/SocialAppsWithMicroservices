using MassTransit;
using MassTransit.Mediator;
using MediaService.Application.UseCases.CreateMedia;
using Shared.Events.Media;
using Shared.Events.PostService;

namespace MediaService.Workers
{
    internal class CreateMedia(IMediator mediator, IPublishEndpoint publishEndpoint) : IConsumer<PostCreatedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<PostCreatedEvent> context)
        {
            if (context.Message.Media.Count == 0) return;
            var client = _mediator.CreateRequestClient<CreateMediaRequest>();
            var response = await client.GetResponse<CreateMediaResponse>(
                new(
                    context.Message.Id,
                    context.Message.Media.Select(
                        x => new CreateMediaRequest_Media(
                            x.ContainerName,
                            x.BlobName,
                            x.Type
                        )
                    )
                )
            );

            for (int i = 0; i < response.Message.Ids.Count; i++)
                await _publishEndpoint.Publish(
                    new MediaCreatedEvent(
                        response.Message.Ids[i],
                        context.Message.Id,
                        context.Message.Media[i].ContainerName,
                        context.Message.Media[i].BlobName,
                        context.Message.Media[i].Type
                    ),
                    context.CancellationToken
                );
        }
    }
}
