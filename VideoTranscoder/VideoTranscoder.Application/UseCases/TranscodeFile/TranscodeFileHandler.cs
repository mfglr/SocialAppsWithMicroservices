using MediatR;

namespace VideoTranscoder.Application.UseCases.TranscodeFile
{
    internal class TranscodeFileHandler(TempDirectoryManager tempDirectoryManager, IVideoTranscoder videoTranscoder) : IRequestHandler<TranscodeFileRequest, byte[]>
    {
        private readonly TempDirectoryManager _tempDirectoryManager = tempDirectoryManager;
        private readonly IVideoTranscoder _videoTranscoder = videoTranscoder;

        public async Task<byte[]> Handle(TranscodeFileRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _tempDirectoryManager.Create();

                var inputStream = request.File.OpenReadStream();

                var inputPath = await _tempDirectoryManager.AddAsync(inputStream, cancellationToken);
                inputStream.Close();
                inputStream.Dispose();

                var trancodedBlobPath = _tempDirectoryManager.GenerateUniqPath("mp4");
                await _videoTranscoder.Transcode(inputPath, trancodedBlobPath, cancellationToken);

                var transcodedBlobStream = File.OpenRead(trancodedBlobPath);
                var bytes = new byte[transcodedBlobStream.Length];
                await transcodedBlobStream.ReadExactlyAsync(bytes,cancellationToken);
                transcodedBlobStream.Close();
                transcodedBlobStream.Dispose();

                _tempDirectoryManager.Delete();

                return bytes;

            }
            catch (Exception)
            {
                _tempDirectoryManager.Delete();
                throw;
            }
        }
    }
}
