using MassTransit;
using MediatR;
using Shared.Events.MediaService;

namespace VideoTranscoder.Application.UseCases.TranscodeVideo
{
    internal class TranscodeVideoHandler(IBlobService blobService, IVideoTranscoder videoTranscoder, TempDirectoryManager tempDirectoryManager, IPublishEndpoint publishEndpoint) : IRequestHandler<TranscodeVideoRequest>
    {
        private readonly IBlobService _blobService = blobService;
        private readonly IVideoTranscoder _videoTranscoder = videoTranscoder;
        private readonly TempDirectoryManager _tempDirectoryManager = tempDirectoryManager;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(TranscodeVideoRequest request, CancellationToken cancellationToken)
        {
            string? blobName = null;
            try
            {
                _tempDirectoryManager.Create();

                var inputStream = await _blobService.GetAsync(request.ContainerName, request.BlobName, cancellationToken);

                var inputPath = await _tempDirectoryManager.AddAsync(inputStream, cancellationToken);
                inputStream.Close();
                inputStream.Dispose();

                var trancodedBlobPath = _tempDirectoryManager.GenerateUniqPath("mp4");
                await _videoTranscoder.Transcode(inputPath, trancodedBlobPath, cancellationToken);

                var transcodedBlobStream = File.OpenRead(trancodedBlobPath);
                blobName = await _blobService.UploadAsync(transcodedBlobStream, request.ContainerName, cancellationToken);
                transcodedBlobStream.Close();
                transcodedBlobStream.Dispose();

                _tempDirectoryManager.Delete();

                await _publishEndpoint.Publish(new VideoTranscodedEvent(request.Id, blobName), cancellationToken);
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
