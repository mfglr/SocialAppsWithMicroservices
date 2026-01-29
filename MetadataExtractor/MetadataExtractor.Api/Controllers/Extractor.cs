using MediatR;
using MetadataExtractor.Application.UseCases.ExtractFileMetadata;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;

namespace MetadataExtractor.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Extractor(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        public Task<Metadata> Extract([FromForm] IFormFile file, CancellationToken cancellationToken) =>
            _sender.Send(new ExtractFileMetadataRequest(file), cancellationToken);
    }
}
