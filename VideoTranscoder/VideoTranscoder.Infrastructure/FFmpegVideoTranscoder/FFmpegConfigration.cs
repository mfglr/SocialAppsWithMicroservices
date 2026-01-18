using Xabe.FFmpeg;

namespace VideoTranscoder.Infrastructure.FFmpegVideoTranscoder
{
    internal static class FFmpegConfigration
    {
        public static void Configure() =>
                FFmpeg.SetExecutablesPath($"{AppContext.BaseDirectory}/FFmpeg");
    }
}
