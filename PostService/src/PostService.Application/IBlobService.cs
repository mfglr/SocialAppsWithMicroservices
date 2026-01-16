using Microsoft.AspNetCore.Http;

namespace PostService.Application
{
    public interface IBlobService
    {
        Task<IReadOnlyList<string>> UploadAsync(string containerName, IFormFileCollection files, CancellationToken cancellationToken);
        Task DeleteAsync(string containerName, IEnumerable<string> blobNames, CancellationToken cancellationToken);
    }
}
