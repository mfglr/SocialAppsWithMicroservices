using Microsoft.AspNetCore.Http;

namespace BlobService.Application.UseCases.UploadBlob
{
    public record UploadBlobRequest(string ContainerName, IFormFileCollection Media);
}
