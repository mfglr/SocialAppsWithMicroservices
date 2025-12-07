using MassTransit;

namespace BlobService.Application.UseCases.UploadBlob
{
    internal class UploadBlobConsumer(IBlobService blobService) : IConsumer<UploadBlobRequest>
    {
        private readonly IBlobService _blobService = blobService;
        public async Task Consume(ConsumeContext<UploadBlobRequest> context)
        {
            var blobNames = await _blobService.UploadAsync(context.Message.ContainerName, context.Message.Media, context.CancellationToken);
            await context.RespondAsync(new UploadBlobResponse(blobNames));
        }
    }
}
