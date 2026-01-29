using Shared.Events;

namespace PostMedia.Application.UseCases.SetPostMediaMetadata
{
    public record SetPostMediaMetadataRequest(Guid Id, string BlobName, Metadata Metadata) : MediatR.IRequest;
}
