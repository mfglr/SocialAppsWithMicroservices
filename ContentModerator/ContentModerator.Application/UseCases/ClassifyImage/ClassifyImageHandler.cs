using MediatR;
using Shared.Objects;

namespace ContentModerator.Application.UseCases.ClassifyImage
{
    internal class ClassifyImageHandler(IImageFrameExtractor imageFrameExtractor, TempDirectoryManager tempDirectoryManager, IBlobService blobService, IModerator moderator) : IRequestHandler<ClassifyImageRequest,ModerationResult>
    {
        public async Task<ModerationResult> Handle(ClassifyImageRequest request, CancellationToken cancellationToken)
        {
            double resolution = 720;
            try
            {
                tempDirectoryManager.Create();
                
                string inputPath;
                using (var inputStream = await blobService.ReadAsync(request.ContainerName, request.BlobName, cancellationToken))
                {
                    inputPath = await tempDirectoryManager.AddAsync(inputStream, cancellationToken);
                }

                var tempPath = tempDirectoryManager.GenerateUniqPath();
                var outputPath = await imageFrameExtractor.ExtractAsync(inputPath, tempPath, resolution, cancellationToken);
                var moderationResult = await moderator.ClassifyImageAsync(outputPath, cancellationToken);

                tempDirectoryManager.Delete();

                return moderationResult;
            }
            catch (Exception)
            {
                tempDirectoryManager.Delete();
                throw;
            }
        }
    }
}
