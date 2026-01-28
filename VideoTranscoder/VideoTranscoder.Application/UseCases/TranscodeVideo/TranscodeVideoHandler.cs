using MediatR;

namespace VideoTranscoder.Application.UseCases.TranscodeVideo
{
    internal class TranscodeVideoHandler(IBlobService blobService, IVideoTranscoder videoTranscoder, TempDirectoryManager tempDirectoryManager) : IRequestHandler<TranscodeVideoRequest, TranscodeVideoResponse>
    {
        public async Task<TranscodeVideoResponse> Handle(TranscodeVideoRequest request, CancellationToken cancellationToken)
        {
            string? blobName = null;
            try
            {
                tempDirectoryManager.Create();

                var inputStream = await blobService.GetAsync(request.ContainerName, request.BlobName, cancellationToken);

                var inputPath = await tempDirectoryManager.AddAsync(inputStream, cancellationToken);
                inputStream.Close();
                inputStream.Dispose();

                var trancodedBlobPath = tempDirectoryManager.GenerateUniqPath("mp4");
                await videoTranscoder.Transcode(inputPath, trancodedBlobPath, cancellationToken);

                using (var transcodedBlobStream = File.OpenRead(trancodedBlobPath))
                {
                    blobName = await blobService.UploadAsync(transcodedBlobStream, request.ContainerName, cancellationToken);
                }

                tempDirectoryManager.Delete();

                return new(blobName);
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
