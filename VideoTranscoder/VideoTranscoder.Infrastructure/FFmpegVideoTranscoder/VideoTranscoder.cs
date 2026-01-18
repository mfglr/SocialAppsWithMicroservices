using VideoTranscoder.Application;
using Xabe.FFmpeg;

namespace VideoTranscoder.Infrastructure.FFmpegVideoTranscoder
{
    internal class VideoTranscoder : IVideoTranscoder
    {
        public async Task Transcode(string inputPath, string outputPath, CancellationToken cancellationToken)
        {
            int resulation = 720;

            var mediaInfo = await FFmpeg.GetMediaInfo(inputPath, cancellationToken);
            var vStream = mediaInfo.VideoStreams.First();
            bool isScaleDownRequired = vStream.Height > vStream.Width ? vStream.Height > resulation : vStream.Width > resulation;
            var fps = vStream.Framerate;

            IConversion conversation = FFmpeg.Conversions.New().AddParameter($"-i \"{inputPath}\"");

            if (isScaleDownRequired)
            {
                var scale = $"scale='if(gt(iw,ih),{resulation},-2)':'if(gt(ih,iw),{resulation},-2)'";
                conversation.AddParameter($"-vf {scale}");
            }

            if (!double.IsNaN(fps) && fps > 30)
                conversation.AddParameter($"-r 30");

            conversation
                .AddParameter("-c:v libx265")
                .AddParameter("-c:a aac")
                .AddParameter("-crf 28")
                .AddParameter("-preset medium")
                .AddParameter($"-movflags +faststart")
                .SetOutput(outputPath);

            await conversation.Start(cancellationToken);
        }
    }
}
