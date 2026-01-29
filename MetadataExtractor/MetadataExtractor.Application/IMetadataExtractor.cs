using Shared.Events;

namespace MetadataExtractor.Application
{
    public interface IMetadataExtractor
    {
        Task<Metadata> ExtractAsync(Stream input, CancellationToken cancellationToken);
    }
}
