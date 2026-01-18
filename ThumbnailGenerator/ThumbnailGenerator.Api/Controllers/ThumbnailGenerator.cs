using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThumbnailGenerator.Application.UseCases.GenerateFileThumbnail;

namespace ThumbnailGenerator.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ThumbnailGenerator(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        public async Task<FileContentResult> Generate([FromForm]GenerateFileThumbnailRequest request, CancellationToken cancellationToken) =>
            File(await _sender.Send(request, cancellationToken),"image/jpeg");
    }
}
