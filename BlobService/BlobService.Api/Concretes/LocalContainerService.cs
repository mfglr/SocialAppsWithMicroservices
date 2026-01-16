using BlobService.Api.Abstracts;
using BlobService.Api.Concretes.Exceptions;

namespace BlobService.Api.Concretes
{
    internal class LocalContainerService(PathFinder pathFinder) : IContainerService
    {
        private readonly PathFinder _pathFinder = pathFinder;


        private string CheckContaier(string containerName)
        {
            var containerPath = _pathFinder.GetContainerPath(containerName);
            if (!Directory.Exists(containerPath))
                throw new ContainerNotFoundException();
            return containerPath;
        }

        public Task CreateAsync(string containerName, CancellationToken cancellationToken)
        {
            var containerPath = _pathFinder.GetContainerPath(containerName);

            if (Directory.Exists(containerPath))
                throw new ContainerAlreadyExistException();

            Directory.CreateDirectory(containerPath);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(string containerName, CancellationToken cancellationToken)
        {
            Directory.Delete(CheckContaier(containerName));
            return Task.CompletedTask;
        }
    }
}
