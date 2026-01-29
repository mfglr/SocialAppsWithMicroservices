using MediatR;
using Shared.Events;

namespace MetadataExtractor.Application.UseCases.ExtractMediaMetadata
{
    public record ExtractMediaMetadataRequest(Guid Id, string ContainerName, string BlobName, MediaType Type) : IRequest<Metadata>;
}
