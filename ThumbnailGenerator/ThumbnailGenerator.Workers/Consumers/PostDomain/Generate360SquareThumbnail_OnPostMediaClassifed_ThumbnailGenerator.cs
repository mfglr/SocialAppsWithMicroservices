using MassTransit;
using MediatR;
using Shared.Events.PostMediaService;
using ThumbnailGenerator.Application.UseCases.GenerateThumbnail;

namespace ThumbnailGenerator.Workers.Consumers.PostDomain
{
    internal class Generate360SquareThumbnail_OnPostMediaClassifed_ThumbnailGenerator(ISender sender, IPublishEndpoint publishEndpoint) : IConsumer<PostMediaClassifiedEvent>
    {
        public async Task Consume(ConsumeContext<PostMediaClassifiedEvent> context)
        {
            if (context.Message.ModerationResult.Sexual != 0) return;

            var thumbnail = await sender.Send(
                new GenerateThumbnailRequest(
                    context.Message.ContainerName,
                    context.Message.BlobName,
                    360,
                    true
                ),
                context.CancellationToken
            );

            await publishEndpoint.Publish(
                new PostMediaThumbnailGeneratedEvent(
                    context.Message.Id,
                    context.Message.BlobName,
                    thumbnail
                ),
                context.CancellationToken
            );
        }
    }
}
