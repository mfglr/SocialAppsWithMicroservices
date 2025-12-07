using BlobService.Application.UseCases.DeleteBlob;
using MassTransit;
using MassTransit.Mediator;
using Shared.Events.PostService;

namespace BlobService.Workers
{
    internal class DeleteBlobs(IMediator mediator) : IConsumer<PostMediaDeletedEvent>
    {
        private readonly IMediator _mediator = mediator;

        public Task Consume(ConsumeContext<PostMediaDeletedEvent> context) =>
            _mediator.Send(
                new DeleteBlobRequest(
                    context.Message.DeletedMedia.ContainerName,
                    context.Message.DeletedMedia.BlobNames
                ),
                context.CancellationToken
            );
    }
}
