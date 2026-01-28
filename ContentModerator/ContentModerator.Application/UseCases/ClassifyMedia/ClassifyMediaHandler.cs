using MediatR;
using Shared.Objects;

namespace ContentModerator.Application.UseCases.ClassifyMedia
{
    internal class ClassifyMediaHandler(IImageFrameExtractor imageFrameExtractor, IVideoFrameExtractor videoFrameExtractor, TempDirectoryManager tempDirectoryManager, IBlobService blobService, IModerator moderator) : IRequestHandler<ClassifyMediaRequest>
    {
        public async Task Handle(ClassifyMediaRequest request, CancellationToken cancellationToken)
        {
            double resolution = 720;
            double fps = 1;

            try
            {
                tempDirectoryManager.Create();

                string inputPath;
                using (var inputStream = await blobService.ReadAsync(request.ContainerName, request.BlobName, cancellationToken))
                {
                    inputPath = await tempDirectoryManager.AddAsync(inputStream, cancellationToken);
                }

                var tempPath = tempDirectoryManager.GenerateUniqPath();
                IEnumerable<string> outputPaths;
                if(request.Type == MediaType.Video)
                    outputPaths = await videoFrameExtractor.ExtractAsync(inputPath, tempPath, resolution, fps, cancellationToken);
                else
                    outputPaths = [await imageFrameExtractor.ExtractAsync(inputPath, tempPath, resolution, cancellationToken)];
                var tasks = outputPaths.Select(path => moderator.ClassifyImageAsync(path, cancellationToken));
                var moderationResult = ModerationResult.Max(await Task.WhenAll(tasks));

                tempDirectoryManager.Delete();
            }
            catch (Exception)
            {
                tempDirectoryManager.Delete();
                throw;
            }

        }
    }
}
