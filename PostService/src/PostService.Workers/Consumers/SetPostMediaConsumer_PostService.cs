using AutoMapper;
using MassTransit;
using MediatR;
using PostService.Application.UseCases.SetPostMedia;
using Shared.Events.MediaService;

namespace PostService.Workers.Consumers
{
    internal class SetPostMediaConsumer_PostService(ISender sender) : IConsumer<MediaPreprocessingCompletedEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<MediaPreprocessingCompletedEvent> context) =>
            _sender
                .Send(
                    new SetPostMediaRequest(
                        context.Message.Id,
                        context.Message.BlobName,
                        context.Message.TranscodedBlobName,
                        context.Message.Metadata,
                        context.Message.ModerationResult,
                        context.Message.Thumbnails
                    ),
                    context.CancellationToken
                );

    }
}
