namespace BlobService.Api.Dtos
{
    public record DeleteBlobRequest(string ContainerName, IEnumerable<string> BlobNames);
}
