using ThumbnailGenerator.Application;
using Xabe.FFmpeg;

namespace ThumbnailGenerator.Infrastructure.FFmpegThumbnailGenerator
{
    internal class ThumbnailGenerator : IThumbnailGenerator
    {
        public async Task GenerateAsync(string input, string output, double resulation, bool isSquare, CancellationToken cancellationToken)
        {
            string filter;
            if (isSquare)
            {
                var shorterSide = "if(gt(iw,ih),ih,iw)";
                var offsetX = $"(iw-{shorterSide})/2";
                var offsetY = $"(ih-{shorterSide})/2";
                var crop = $"crop='{shorterSide}:{shorterSide}:{offsetX}:{offsetY}'";

                var calculatedResulation = $"if(gt(iw,ih),if(gt({resulation},ih),ih,{resulation}),if(gt({resulation},iw),iw,{resulation}))";
                var scale = $"scale='{calculatedResulation}:{calculatedResulation}'";

                filter = $"{crop},{scale}";
            }
            else
                filter = $"scale='if(gt(iw,ih),if(gt({resulation},iw),iw,{resulation}),-2)':'if(gt(ih,iw),if(gt({resulation},ih),ih,{resulation}),-2)'";

            await FFmpeg.Conversions.New()
                .AddParameter($"-i \"{input}\"")
                .AddParameter($"-vf {filter}")
                .AddParameter("-vframes 1")
                .SetOutput(output)
                .Start(cancellationToken);
        }
    }
}
