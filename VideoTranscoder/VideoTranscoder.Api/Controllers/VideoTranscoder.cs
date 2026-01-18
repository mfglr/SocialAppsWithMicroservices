using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoTranscoder.Application.UseCases.TranscodeFile;

namespace VideoTranscoder.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VideoTranscoder(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        public async Task<FileResult> Transcode([FromForm]TranscodeFileRequest request, CancellationToken cancellationToken) =>
            File(await _sender.Send(request, cancellationToken), "video/mp4");
    }
}
