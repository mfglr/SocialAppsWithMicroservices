using Shared.Events;

namespace PostService.Application.UseCases.GetPostById
{
    public record GetPostByIdResponse_Content(
        string Value,
        ModerationResult ModerationResult
    );
    public record GetPostByIdResponse_Media(
        string ContainerName,
        string BlobName,
        MediaType Type,
        Metadata? Metadata,
        ModerationResult? ModerationResult,
        IEnumerable<Thumbnail> Thumbnails,
        string? TranscodedBlobName
    );
    public record GetPostByIdResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid UserId,
        int Version,
        bool IsDeleted,
        GetPostByIdResponse_Content? Content,
        IEnumerable<GetPostByIdResponse_Media> Media
    );
}
