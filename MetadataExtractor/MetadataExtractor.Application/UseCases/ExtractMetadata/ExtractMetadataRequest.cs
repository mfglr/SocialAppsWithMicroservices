using MediatR;
using Shared.Objects;

namespace MetadataExtractor.Application.UseCases.ExtractMetadata
{
    public record ExtractMetadataRequest(string ContainerName, string BlobName) : IRequest<Metadata>;
}
