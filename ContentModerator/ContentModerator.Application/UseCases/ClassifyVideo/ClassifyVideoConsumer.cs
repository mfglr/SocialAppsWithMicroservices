using MassTransit;
using Shared.Objects;

namespace ContentModerator.Application.UseCases.ClassifyVideo
{
    internal class ClassifyVideoConsumer(TempDirectoryManager tempDirectoryManager, IModerator nsfwScoreCalculator, IVideoFrameExtractor extractor, IBlobService blobService) : IConsumer<ClassifyVideoRequest>
    {
        private readonly TempDirectoryManager _tempDirectoryManager = tempDirectoryManager;
        private readonly IModerator _nsfwScoreCalculator = nsfwScoreCalculator;
        private readonly IVideoFrameExtractor _extractor = extractor;
        private readonly IBlobService _blobService = blobService;

        public async Task Consume(ConsumeContext<ClassifyVideoRequest> context)
        {
            try
            {
                _tempDirectoryManager.Create();

                var inputStream = await _blobService.ReadAsync(context.Message.ContainerName, context.Message.BlobName, context.CancellationToken);

                var inputPath = await _tempDirectoryManager.AddAsync(inputStream, context.CancellationToken);
                inputStream.Close();
                inputStream.Dispose();

                var outputPath = _tempDirectoryManager.GenerateUniqPath();
                var paths = await _extractor.ExtractAsync(inputPath, outputPath, 720, 1, context.CancellationToken);

                var tasks = paths.Select(path => _nsfwScoreCalculator.ClassifyImageAsync(path, context.CancellationToken));
                var results = await Task.WhenAll(tasks);

                _tempDirectoryManager.Delete();

                await context.RespondAsync(ModerationResult.Max(results));
            }
            catch (Exception)
            {
                _tempDirectoryManager.Delete();
                throw;
            }
        }
    }
}
