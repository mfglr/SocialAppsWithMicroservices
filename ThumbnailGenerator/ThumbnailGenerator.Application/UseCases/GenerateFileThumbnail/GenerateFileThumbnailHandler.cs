using MediatR;

namespace ThumbnailGenerator.Application.UseCases.GenerateFileThumbnail
{
    internal class GenerateFileThumbnailHandler(TempDirectoryManager tempDirectoryManager, IThumbnailGenerator thumbnailGenerator) : IRequestHandler<GenerateFileThumbnailRequest, byte[]>
    {
        private readonly TempDirectoryManager _tempDirectoryManager = tempDirectoryManager;
        private readonly IThumbnailGenerator _thumbnailGenerator = thumbnailGenerator;

        public async Task<byte[]> Handle(GenerateFileThumbnailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _tempDirectoryManager.Create();

                var inputStream = request.File.OpenReadStream();
                var inputPath = await _tempDirectoryManager.AddAsync(inputStream, cancellationToken);
                inputStream.Close();
                inputStream.Dispose();

                var outputPath = _tempDirectoryManager.GenerateUniqPath("jpeg");
                await _thumbnailGenerator.GenerateAsync(inputPath, outputPath, request.Resolution, request.IsSquare, cancellationToken);

                var stream = File.OpenRead(outputPath);
                var bytes = new byte[stream.Length];
                await stream.ReadExactlyAsync(bytes, cancellationToken);
                stream.Close();
                stream.Dispose();

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
