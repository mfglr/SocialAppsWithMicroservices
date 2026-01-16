using Microsoft.Extensions.Configuration;

namespace BlobService.Api.Concretes
{
    internal class PathFinder(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public string GetContainerPath(string containerName)
            => $"{_configuration["RootPath"]}/Blobs/{containerName}";
        
        public string GetPath(string containerName, string blobName)
            => $"{_configuration["RootPath"]}/Blobs/{containerName}/{blobName}";
    }
}
