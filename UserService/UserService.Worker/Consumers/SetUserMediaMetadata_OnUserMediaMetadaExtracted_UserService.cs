using MassTransit;
using MediatR;
using Shared.Events.UserService;
using UserService.Application.UseCases.SetMediaMetadata;

namespace UserService.Worker.Consumers
{
    internal class SetUserMediaMetadata_OnUserMediaMetadaExtracted_UserService(ISender sender) : IConsumer<UserMediaMetadataExtractedEvent>
    {
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<UserMediaMetadataExtractedEvent> context) =>
            _sender
                .Send(
                    new SetMediaMetadataRequest(context.Message.Id, context.Message.BlobName, context.Message.Metadata),
                    context.CancellationToken
                );
    }
}
