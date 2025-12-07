using MassTransit;

namespace BlobService.Application.UseCases.DeleteBlob
{
    internal class DeleteBlobConsumer(IBlobService blobService) : IConsumer<DeleteBlobRequest>
    {
        private readonly IBlobService _blobService = blobService;

        public Task Consume(ConsumeContext<DeleteBlobRequest> context) =>
            _blobService.DeleteAsync(
                context.Message.ContainerName,
                context.Message.BlobNames,
                context.CancellationToken
            );
    }
}
