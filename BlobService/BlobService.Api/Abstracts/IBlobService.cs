namespace BlobService.Api.Abstracts
{
    public interface IBlobService
    {
        Task<IEnumerable<string>> UploadAsync(string containerName, IFormFileCollection media, CancellationToken cancellationToken);
        Task<string> UploadAsync(string containerName, IFormFile media, CancellationToken cancellationToken);
        Task<Stream> ReadAsync(string containerName, string blobName, CancellationToken cancellationToken);
        Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken);
        Task DeleteAsync(string containerName, IEnumerable<string> blobNames, CancellationToken cancellationToken);

        Task UploadAsync(IFormFile media, string containerName, string blobName, CancellationToken cancellationToken);
        Task UploadAsync(Stream stream, string containerName, string blobName, CancellationToken cancellationToken);
        Task<bool> Exist(string containerName, string blobName, CancellationToken cancellationToken);
    }
}
