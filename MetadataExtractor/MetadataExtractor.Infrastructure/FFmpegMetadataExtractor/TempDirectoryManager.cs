namespace MetadataExtractor.Infrastructure.FFmpegMetadataExtractor
{
    internal class TempDirectoryManager
    {
        public string ScopeContainerName { get; private set; }

        private static string GetContainerPath(string containerName)
            => $"{AppContext.BaseDirectory}Blobs/{containerName}";

        private static string GetPath(string containerName, string blobName)
            => $"{AppContext.BaseDirectory}Blobs/{containerName}/{blobName}";

        private static string GenerateUniqName(string? extention = null)
        {
            if (extention == null)
                return $"{Guid.NewGuid()}_{DateTime.UtcNow.Ticks}";
            return $"{Guid.NewGuid()}_{DateTime.UtcNow.Ticks}.{extention}";
        }

        public TempDirectoryManager() => ScopeContainerName = $"Temp/{GenerateUniqName()}";

        public string GenerateUniqPath(string? extention = null) => 
            GetPath(ScopeContainerName, GenerateUniqName(extention));

        public void Create() =>
            Directory.CreateDirectory(GetContainerPath(ScopeContainerName));

        public async Task<string> AddAsync(Stream stream, CancellationToken cancellationToken)
        {
            var path = GetPath(ScopeContainerName, GenerateUniqName());
            using var fileStream = File.Create(path);
            await stream.CopyToAsync(fileStream, cancellationToken);
            return path;
        }

        public void Delete()
        {
            var path = GetContainerPath(ScopeContainerName);
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }
    }
}
