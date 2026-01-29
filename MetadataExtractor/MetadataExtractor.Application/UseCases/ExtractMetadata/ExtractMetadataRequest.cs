using MediatR;
using Shared.Events;

namespace MetadataExtractor.Application.UseCases.ExtractMetadata
{
    public record ExtractMetadataRequest(string ContainerName, string BlobName) : IRequest<Metadata>;
}
