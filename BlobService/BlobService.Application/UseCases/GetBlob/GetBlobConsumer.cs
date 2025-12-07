using MassTransit;

namespace BlobService.Application.UseCases.GetBlob
{
    internal class GetBlobConsumer(IBlobService blobService) : IConsumer<GetBlobRequest>
    {
        private readonly IBlobService _blobService = blobService;

        public async Task Consume(ConsumeContext<GetBlobRequest> context)
        {
            var stream = await _blobService.ReadAsync(context.Message.ContainerName, context.Message.BlobName, context.CancellationToken);
            await context.RespondAsync(new GetBlobResponse(stream));
        }
    }
}
