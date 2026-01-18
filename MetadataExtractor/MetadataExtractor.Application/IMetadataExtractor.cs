using Shared.Objects;

namespace MetadataExtractor.Application
{
    public interface IMetadataExtractor
    {
        Task<Metadata> ExtractAsync(Stream input, CancellationToken cancellationToken);
    }
}
