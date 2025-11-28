using MassTransit;
using MassTransit.Mediator;
using MassTransit.Transports;
using Shared.Events.Media;
using ThumbnailGenerator.Application.UseCases.GenerateThumbnail;

namespace ThumbnailGenerator.Workers
{
    internal class Generate360SquareThumbnail(IMediator mediator, IPublishEndpoint publishEndpoint) : IConsumer<MediaCreatedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Consume(ConsumeContext<MediaCreatedEvent> context)
        {
            var client = _mediator.CreateRequestClient<GenerateThumbnailRequest>();
            var response = await client.GetResponse<GenerateThumbnailResponse>(
                new(context.Message.ContainerName, context.Message.BlobName, 360, true)
            );
            
            await _publishEndpoint.Publish(
                new MediaThumbnailGeneratedEvent(context.Message.Id, new (response.Message.BlobName, 360, true)),
                context.CancellationToken
            );
        }
    }
}
