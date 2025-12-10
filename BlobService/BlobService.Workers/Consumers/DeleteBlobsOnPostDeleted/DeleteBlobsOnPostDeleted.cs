using BlobService.Application.UseCases.DeleteBlob;
using MassTransit;
using MassTransit.Mediator;
using Shared.Events.PostService;

namespace BlobService.Workers.Consumers.DeleteBlobsOnPostDeleted
{
    internal class DeleteBlobsOnPostDeleted(IMediator mediator) : IConsumer<PostDeletedEvent>
    {
        private readonly IMediator _mediator = mediator;

        public Task Consume(ConsumeContext<PostDeletedEvent> context) =>
            _mediator.Send(
                DeleteBlobsOnPostDeletedMapper.Map(context.Message),
                context.CancellationToken
            );
    }
}
