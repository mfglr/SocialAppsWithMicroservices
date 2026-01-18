using Xabe.FFmpeg;

namespace MetadataExtractor.Infrastructure.FFmpegMetadataExtractor
{
    public static class FFmpegConfigration
    {
        public static void Configure() =>
                FFmpeg.SetExecutablesPath($"{AppContext.BaseDirectory}/FFmpeg");
    }
}
