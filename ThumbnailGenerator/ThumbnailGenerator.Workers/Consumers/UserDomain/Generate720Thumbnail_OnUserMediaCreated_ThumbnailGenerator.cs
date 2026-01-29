using MassTransit;
using MediatR;
using Shared.Events.UserService;
using ThumbnailGenerator.Application.UseCases.GenerateThumbnail;

namespace ThumbnailGenerator.Workers.Consumers.UserDomain
{
    internal class Generate720Thumbnail_OnUserMediaCreated_ThumbnailGenerator(ISender sender, IPublishEndpoint publishEndpoint) : IConsumer<UserMediaCreatedEvent>
    {
        public async Task Consume(ConsumeContext<UserMediaCreatedEvent> context)
        {
            var thumbnail = await sender
                .Send(
                    new GenerateThumbnailRequest(
                        context.Message.ContainerName,
                        context.Message.BlobName,
                        720,
                        false
                    ),
                    context.CancellationToken
                );

            await publishEndpoint.Publish(
                new UserMediaThumbnailGeneratedEvent(
                    context.Message.Id,
                    context.Message.BlobName,
                    thumbnail
                ),
                context.CancellationToken
            );
        }
    }
}
