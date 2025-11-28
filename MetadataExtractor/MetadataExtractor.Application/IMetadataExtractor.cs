using Shared.Objects;

namespace MetadataExtractor.Application
{
    public interface IMetadataExtractor
    {
        Task<Metadata> Extract(string input, string tempPath, CancellationToken cancellationToken);
    }
}
