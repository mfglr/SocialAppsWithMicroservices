using BlobService.Application.UseCases.DeleteBlob;
using BlobService.Application.UseCases.GetBlob;
using BlobService.Application.UseCases.UploadBlob;
using BlobService.Application.UseCases.UploadSingleBlob;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BlobService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlobsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [RequestSizeLimit(104857600)]
        [HttpPost]
        public async Task<IEnumerable<string>> Upload([FromForm]string containerName, [FromForm]IFormFileCollection media, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<UploadBlobRequest>();
            var response = await client.GetResponse<UploadBlobResponse>(new UploadBlobRequest(containerName, media),cancellationToken: cancellationToken);
            return response.Message.BlobNames;
        }

        [RequestSizeLimit(104857600)]
        [HttpPost]
        public async Task UploadSingleBlob([FromForm] string containerName, [FromForm] string blobName, [FromForm] IFormFile media, CancellationToken cancellationToken) =>
            await _mediator.Send(new UploadSingleBlobRequest(containerName, blobName, media), cancellationToken);

        [HttpPost]
        public async Task Delete(DeleteBlobRequest request, CancellationToken cancellationToken)
             => await _mediator.Send(request,cancellationToken);

        [HttpGet("{containerName}/{blobName}")]
        public async Task<Stream> Get(string containerName, string blobName, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<GetBlobRequest>();
            var response = await client.GetResponse<GetBlobResponse>(new GetBlobRequest(containerName,blobName), cancellationToken: cancellationToken);
            return response.Message.Stream;
        }
    }
}
