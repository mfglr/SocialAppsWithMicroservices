using MetadataExtractor.Application;
using Shared.Events;
using Xabe.FFmpeg;

namespace MetadataExtractor.Infrastructure.FFmpegMetadataExtractor
{
    internal class FFmpegMetadataExtractor(TempDirectoryManager tempDirectoryManager) : IMetadataExtractor
    {
        private readonly TempDirectoryManager _tempDirectoryManager = tempDirectoryManager;

        private async Task<Metadata> ExtractAsync(string input, string tempPath, CancellationToken cancellationToken)
        {
            await FFmpeg.Conversions.New()
                    .AddParameter($"-i \"{input}\"")
                    .AddParameter($"-frames:v 1")
                    .SetOutput(tempPath)
                    .Start(cancellationToken);

            var info = await FFmpeg.GetMediaInfo(input, cancellationToken);
            var vStream = info.VideoStreams.First();

            return new(vStream.Width, vStream.Height, info.Duration.TotalSeconds);
        }

        public async Task<Metadata> ExtractAsync(Stream input, CancellationToken cancellationToken)
        {
            try
            {
                _tempDirectoryManager.Create();

                var inputPath = await _tempDirectoryManager.AddAsync(input, cancellationToken);
                var tempPath = _tempDirectoryManager.GenerateUniqPath("jpeg");
                var metadata = await ExtractAsync(inputPath, tempPath, cancellationToken);

                _tempDirectoryManager.Delete();
                
                return metadata;
            }
            catch (Exception)
            {
                _tempDirectoryManager.Delete();
                throw;
            }
        }
    }
}
