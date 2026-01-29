using MediatR;
using Shared.Events;

namespace ThumbnailGenerator.Application.UseCases.GenerateThumbnail
{
    internal class GenerateThumbnailHandler(IThumbnailGenerator thumbnailGenerator, IBlobService blobService, TempDirectoryManager tempDirectoryManager) : IRequestHandler<GenerateThumbnailRequest, Thumbnail>
    {
        public async Task<Thumbnail> Handle(GenerateThumbnailRequest request, CancellationToken cancellationToken)
        {
            string? blobName = null;
            try
            {
                tempDirectoryManager.Create();

                string inputPath;
                using (var inputStream = await blobService.GetAsync(request.ContainerName, request.BlobName, cancellationToken))
                {
                    inputPath = await tempDirectoryManager.AddAsync(inputStream, cancellationToken);
                }

                var outputPath = tempDirectoryManager.GenerateUniqPath("jpeg");
                await thumbnailGenerator.GenerateAsync(inputPath, outputPath, request.Resulation, request.IsSquare, cancellationToken);

                using (var fileStream = File.OpenRead(outputPath))
                {
                    blobName = await blobService.UploadAsync(fileStream, request.ContainerName, cancellationToken);
                }

                tempDirectoryManager.Delete();

                return new(blobName, request.Resulation, request.IsSquare);
            }
            catch (Exception)
            {
                tempDirectoryManager.Delete();
                if (blobName != null)
                    await blobService.DeleteAsync(request.ContainerName, blobName, cancellationToken);
                throw;
            }
        }
    }
}
