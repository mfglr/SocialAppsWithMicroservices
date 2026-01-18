using MassTransit;
using MediatR;
using Shared.Events.MediaService;
using Shared.Objects;

namespace ThumbnailGenerator.Application.UseCases.GenerateThumbnail
{
    internal class GenerateThumbnailHandler(IThumbnailGenerator thumbnailGenerator, IBlobService blobService, TempDirectoryManager tempDirectoryManager, IPublishEndpoint publishEndpoint) : IRequestHandler<GenerateThumbnailRequest>
    {
        private readonly IThumbnailGenerator _thumbnailGenerator = thumbnailGenerator;
        private readonly IBlobService _blobService = blobService;
        private readonly TempDirectoryManager _tempDirectoryManager = tempDirectoryManager;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(GenerateThumbnailRequest request, CancellationToken cancellationToken)
        {
            string? blobName = null;
            try
            {
                _tempDirectoryManager.Create();

                var inputStream = await _blobService.GetAsync(request.ContainerName, request.BlobName, cancellationToken);
                var inputPath = await _tempDirectoryManager.AddAsync(inputStream, cancellationToken);
                inputStream.Close();
                inputStream.Dispose();

                var outputPath = _tempDirectoryManager.GenerateUniqPath("jpeg");
                await _thumbnailGenerator.GenerateAsync(inputPath, outputPath, request.Resulation, request.IsSquare, cancellationToken);

                var fileStream = File.OpenRead(outputPath);
                blobName = await _blobService.UploadAsync(fileStream, request.ContainerName, cancellationToken);
                fileStream.Close();
                fileStream.Dispose();

                _tempDirectoryManager.Delete();
                
                await _publishEndpoint.Publish(
                    new MediaThumbnailGeneratedEvent(
                        request.Id,
                        new Thumbnail(blobName, request.Resulation, request.IsSquare)
                    ),
                    cancellationToken
                );
            }
            catch (Exception)
            {
                _tempDirectoryManager.Delete();
                if (blobName != null)
                    await _blobService.DeleteAsync(request.ContainerName, blobName, cancellationToken);
                throw;
            }
        }
    }
}
