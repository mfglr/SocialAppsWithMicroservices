using Shared.Objects;

namespace MediaService.Application.UseCases.SetMediaMetadata
{
    public record SetMediaMetadataRequest(Guid Id, Metadata Metadata);
}
