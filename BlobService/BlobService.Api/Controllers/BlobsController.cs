using BlobService.Api.Abstracts;
using BlobService.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BlobService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlobsController(IBlobService blobService) : ControllerBase
    {
        private readonly IBlobService _blobService = blobService;

        [RequestSizeLimit(104857600)]
        [HttpPost]
        public Task<IEnumerable<string>> Upload([FromForm]string containerName, [FromForm]IFormFileCollection media, CancellationToken cancellationToken) =>
            _blobService.UploadAsync(containerName, media, cancellationToken);

        [HttpPost]
        public Task Delete(DeleteBlobRequest request, CancellationToken cancellationToken) =>
            _blobService.DeleteAsync(request.ContainerName, request.ContainerName, cancellationToken);

        [HttpGet("{containerName}/{blobName}")]
        public Task<Stream> Get(string containerName, string blobName, CancellationToken cancellationToken) =>
            _blobService.ReadAsync(containerName, blobName, cancellationToken);
    }
}
