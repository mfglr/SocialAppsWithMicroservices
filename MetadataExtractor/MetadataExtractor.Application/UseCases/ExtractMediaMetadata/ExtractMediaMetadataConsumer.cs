using MassTransit;

namespace MetadataExtractor.Application.UseCases.ExtractMediaMetadata
{
    internal class ExtractMediaMetadataConsumer(TempDirectoryManager tempDirectoryManager, IBlobService blobService,IMetadataExtractor extractor) : IConsumer<ExtractMediaMetadataRequest>
    {
        private readonly TempDirectoryManager _tempDirectoryManager = tempDirectoryManager;
        private readonly IBlobService _blobService = blobService;
        private readonly IMetadataExtractor _extractor = extractor;

        public async Task Consume(ConsumeContext<ExtractMediaMetadataRequest> context)
        {
            try
            {
                _tempDirectoryManager.Create();
                var inputStream = await _blobService.ReadAsync(context.Message.ContainerName, context.Message.BlobName, context.CancellationToken);

                var inputPath = await _tempDirectoryManager.AddAsync(inputStream, context.CancellationToken);
                inputStream.Close();
                inputStream.Dispose();

                var tempPath = _tempDirectoryManager.GenerateUniqPath("jpeg");
                var metadata = await _extractor.Extract(inputPath, tempPath, context.CancellationToken);

                await context.RespondAsync(metadata);

                _tempDirectoryManager.Delete();

            }
            catch (Exception)
            {
                _tempDirectoryManager.Delete();
                throw;
            }
        }
    }
}
