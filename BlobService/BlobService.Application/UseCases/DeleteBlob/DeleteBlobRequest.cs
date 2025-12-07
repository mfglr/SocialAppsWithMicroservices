namespace BlobService.Application.UseCases.DeleteBlob
{
    public record DeleteBlobRequest(string ContainerName, IEnumerable<string> BlobNames);
}
