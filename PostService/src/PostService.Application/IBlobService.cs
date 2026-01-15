using Microsoft.AspNetCore.Http;

namespace PostService.Application
{
    public interface IBlobService
    {
        Task<string> UploadAsync(string containerName, IFormFile file, CancellationToken cancellationToken);
        Task<IReadOnlyList<string>> UploadAsync(string containerName, IFormFileCollection files, CancellationToken cancellationToken);
        Task DeleteAsync(string containerName, IEnumerable<string> blobNames, CancellationToken cancellationToken);
    }
}
