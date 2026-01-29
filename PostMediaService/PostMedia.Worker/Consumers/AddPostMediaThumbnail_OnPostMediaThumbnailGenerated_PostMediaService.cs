using MassTransit;
using MediatR;
using PostMedia.Application.UseCases.AddPostMediaThumbnail;
using Shared.Events.PostMediaService;

namespace PostMedia.Worker.Consumers
{
    internal class AddPostMediaThumbnail_OnPostMediaThumbnailGenerated_PostMediaService(ISender sender) : IConsumer<PostMediaThumbnailGeneratedEvent>
    {
        public Task Consume(ConsumeContext<PostMediaThumbnailGeneratedEvent> context) =>
            sender
                .Send(
                    new AddPostMediaThumbnailRequest(
                        context.Message.Id,
                        context.Message.BlobName,
                        context.Message.Thumbnail
                    ),
                    context.CancellationToken
                );
    }
}
