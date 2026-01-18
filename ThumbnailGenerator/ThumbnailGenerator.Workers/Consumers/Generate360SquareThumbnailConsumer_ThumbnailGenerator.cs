using MassTransit;
using MediatR;
using Shared.Events.MediaService;
using ThumbnailGenerator.Application.UseCases.GenerateThumbnail;

namespace ThumbnailGenerator.Workers.Consumers
{
    internal class Generate360SquareThumbnailConsumer_ThumbnailGenerator(ISender sender) : IConsumer<MediaCreatedEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<MediaCreatedEvent> context) =>
            _sender
                .Send(
                    new GenerateThumbnailRequest(
                        context.Message.Id,
                        context.Message.ContainerName,
                        context.Message.BlobName,
                        360,
                        true
                    ),
                    context.CancellationToken
                );
    }
}
