using Shared.Events;

namespace PostMedia.Application.UseCases.CreatePost
{
    public record CreatePostRequest_Media(
        string ContainerName,
        string BlobName,
        MediaType Type
    );
    public record CreatePostRequest(
        Guid Id,
        IEnumerable<CreatePostRequest_Media> Media
    ) : MediatR.IRequest;
}
