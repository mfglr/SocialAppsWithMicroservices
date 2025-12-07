using Shared.Objects;

namespace QueryService.Application.UseCases.PostUseCases.GetPostById
{
    public record GetPostByIdResponse_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record GetPostByIdResponse_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        string? TranscodedBlobName,
        Metadata Metadata,
        ModerationResult ModerationResult,
        IEnumerable<Thumbnail> ThumbNails
    );
    public record GetPostByIdResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        GetPostByIdResponse_Content? Content,
        IEnumerable<GetPostByIdResponse_Media> Media
    );
}
