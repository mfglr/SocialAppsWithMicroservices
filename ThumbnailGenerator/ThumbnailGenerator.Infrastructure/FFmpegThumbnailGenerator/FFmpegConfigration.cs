using Xabe.FFmpeg;

namespace ThumbnailGenerator.Infrastructure.FFmpegThumbnailGenerator
{
    internal static class FFmpegConfigration
    {
        public static void Configure() =>
                FFmpeg.SetExecutablesPath($"{AppContext.BaseDirectory}/FFmpeg");
    }
}
