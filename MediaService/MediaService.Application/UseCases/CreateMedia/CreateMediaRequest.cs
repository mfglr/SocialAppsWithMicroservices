using Shared.Objects;

namespace MediaService.Application.UseCases.CreateMedia
{
    public record CreateMediaRequest_Media(string ContainerName, string BlobName, MediaType Type);
    public record CreateMediaRequest(Guid Id, IEnumerable<CreateMediaRequest_Media> Media);
}
