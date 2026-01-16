namespace BlobService.Api.Abstracts
{
    public interface IContainerService
    {
        Task CreateAsync(string containerName, CancellationToken cancellationToken);
        Task DeleteAsync(string containerName, CancellationToken cancellationToken);
    }
}
