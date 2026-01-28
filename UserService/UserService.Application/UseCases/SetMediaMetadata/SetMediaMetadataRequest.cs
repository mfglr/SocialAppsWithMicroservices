using Shared.Objects;

namespace UserService.Application.UseCases.SetMediaMetadata
{
    public record SetMediaMetadataRequest(Guid Id, string BlobName, Metadata Metadata) : MediatR.IRequest;
}
