using MediatR;
using Shared.Objects;

namespace MetadataExtractor.Application.UseCases.ExtractMediaMetadata
{
    public record ExtractMediaMetadataRequest(Guid Id, string ContainerName, string BlobName, MediaType Type) : IRequest;
}
