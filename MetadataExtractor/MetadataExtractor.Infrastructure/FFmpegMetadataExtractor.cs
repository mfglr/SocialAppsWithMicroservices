using MetadataExtractor.Application;
using Shared.Objects;
using Xabe.FFmpeg;

namespace MetadataExtractor.Infrastructure
{
    internal class FFmpegMetadataExtractor : IMetadataExtractor
    {
        public async Task<Metadata> Extract(string input, string tempPath, CancellationToken cancellationToken)
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
    }
}
