using ContentModerator.Application.UseCases.ClassifyMedia;
using MassTransit;
using MediatR;
using Shared.Events.MediaService;

namespace ContentModerator.Worker.Consumers
{
    internal class ClassifyMedia_OnMediaMetadataExtractionSuccess_ContentModerator(ISender sender) : IConsumer<MediaMetadataExtractionSuccessEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<MediaMetadataExtractionSuccessEvent> context) =>
            _sender
                .Send(
                    new ClassifyMediaRequest(
                        context.Message.Id,
                        context.Message.ContainerName,
                        context.Message.BlobName,
                        context.Message.Type
                ), 
                context.CancellationToken
            );
        
    }
}
