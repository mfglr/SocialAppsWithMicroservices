using BlobService.Api.Abstracts;
using BlobService.Api.Concretes.Exceptions;

namespace BlobService.Api.Concretes
{
    internal class LocalBlobService(PathFinder pathFinder, UniqNameGenerator uniqNameGenerator) : IBlobService
    {
        private readonly PathFinder _pathFinder = pathFinder;
        private readonly UniqNameGenerator _uniqNameGenerator = uniqNameGenerator;

        private string CheckContainer(string containerName)
        {
            var containerPath = _pathFinder.GetContainerPath(containerName);
            if (!Directory.Exists(containerPath))
                throw new ContainerNotFoundException();
            return containerPath;
        }
        private string CheckBlob(string containerName, string blobName)
        {
            CheckContainer(containerName);
            var path = _pathFinder.GetPath(containerName, blobName);
            if (!File.Exists(path))
                throw new BlobNotFoundException();
            return path;
        }

        public Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken)
        {
            File.Delete(CheckBlob(containerName, blobName));
            return Task.CompletedTask;
        }
        public Task DeleteAsync(string containerName, IEnumerable<string> blobNames, CancellationToken cancellationToken)
        {
            List<string> paths = [];
            foreach(var blobName in blobNames)
                paths.Add(CheckBlob(containerName, blobName));

            foreach(var path in paths)
                File.Delete(path);

            return Task.CompletedTask;
        }

        public Task<bool> Exist(string containerName, string blobName, CancellationToken cancellationToken)
        {
            CheckContainer(containerName);
            return Task.FromResult(File.Exists(_pathFinder.GetPath(containerName, blobName)));
        }

        public async Task<Stream> ReadAsync(string containerName, string blobName, CancellationToken cancellationToken)
            => await Task.FromResult(new FileStream(CheckBlob(containerName, blobName), FileMode.Open, FileAccess.Read));

        public async Task UploadAsync(Stream stream, string containerName, string blobName, CancellationToken cancellationToken)
        {
            CheckContainer(containerName);

            var path = _pathFinder.GetPath(containerName, blobName);
            if (File.Exists(path))
                throw new BlobNameAlreadyExistsException();

            using var file = File.OpenWrite(path);
            await stream.CopyToAsync(file, cancellationToken);
        }
        public async Task UploadAsync(IFormFile media, string containerName, string blobName, CancellationToken cancellationToken)
        {
            CheckContainer(containerName);

            using var stream = media.OpenReadStream();
            var path = _pathFinder.GetPath(containerName, blobName);
            if (File.Exists(path))
                throw new BlobNameAlreadyExistsException();

            using var file = File.OpenWrite(path);
            await stream.CopyToAsync(file, cancellationToken);
        }
        public async Task<IEnumerable<string>> UploadAsync(string containerName, IFormFileCollection media, CancellationToken cancellationToken)
        {
            CheckContainer(containerName);

            List<string> blobNames = [];
            try
            {
                foreach (var item in media)
                {
                    var blobName = _uniqNameGenerator.Generate();
                    blobNames.Add(blobName);
                    var path = _pathFinder.GetPath(containerName, blobName);
                    using var stream = item.OpenReadStream();
                    using var file = File.OpenWrite(path);
                    await stream.CopyToAsync(file, cancellationToken);
                }
                return blobNames;
            }
            catch (Exception)
            {
                foreach(var blobName in blobNames)
                {
                    var path = _pathFinder.GetPath(containerName, blobName);
                    if(File.Exists(path))
                        File.Delete(path);
                }
                throw;
            }
        }

        public async Task<string> UploadAsync(string containerName, IFormFile media, CancellationToken cancellationToken)
        {
            CheckContainer(containerName);
            
            var blobName = _uniqNameGenerator.Generate();
            string path = _pathFinder.GetPath(containerName, blobName);
            using var stream = media.OpenReadStream();
            using var file = File.OpenWrite(path);
            await stream.CopyToAsync(file, cancellationToken);
            return blobName;
        }

    }
}
