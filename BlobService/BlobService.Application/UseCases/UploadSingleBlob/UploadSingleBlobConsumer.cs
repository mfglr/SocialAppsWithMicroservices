using MassTransit;

namespace BlobService.Application.UseCases.UploadSingleBlob
{
    internal class UploadSingleBlobConsumer(IBlobService blobService) : IConsumer<UploadSingleBlobRequest>
    {
        private readonly IBlobService _blobService = blobService;

        public Task Consume(ConsumeContext<UploadSingleBlobRequest> context)
            => _blobService.UploadAsync(
                context.Message.Media,
                context.Message.ContainerName,
                context.Message.BlobName,
                context.CancellationToken
            );
    }
}
