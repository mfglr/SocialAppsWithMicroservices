using MediatR;
using Shared.Events;

namespace PostService.Application.UseCases.SetPostMedia
{
    public record SetPostMediaRequest_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails,
        string? TranscodedBlobName
    );
    public record SetPostMediaRequest(
        Guid Id,
        IEnumerable<SetPostMediaRequest_Media> Media
    ) : IRequest;
}
