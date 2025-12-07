namespace BlobService.Application.UseCases.GetBlob
{
    public record GetBlobRequest(string ContainerName, string BlobName);
}
