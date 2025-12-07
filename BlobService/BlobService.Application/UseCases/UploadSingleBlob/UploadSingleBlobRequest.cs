using Microsoft.AspNetCore.Http;

namespace BlobService.Application.UseCases.UploadSingleBlob
{
    public record UploadSingleBlobRequest(string ContainerName, string BlobName, IFormFile Media);
}
