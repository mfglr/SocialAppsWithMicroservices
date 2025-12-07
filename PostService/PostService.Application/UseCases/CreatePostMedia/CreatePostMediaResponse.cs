using Shared.Objects;

namespace PostService.Application.UseCases.CreatePostMedia
{
    public record CreatePostMediaResponse_Media(string ContainerName, string BlobName, MediaType Type);
    public record CreatePostMediaResponse(Guid Id, IEnumerable<CreatePostMediaResponse_Media> Media);
}
