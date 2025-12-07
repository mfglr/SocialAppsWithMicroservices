using PostService.Domain;

namespace PostService.Application.UseCases.DeletePostMedia
{
    internal class DeletePostMediaMapper
    {
        private static DeletePostMediaResponse_Content? Map(Content? content) =>
            content != null
                ? new(content.Value, content.ModerationResult)
                : null;

        private static DeletePostMediaResponse_Media Map(Media media) =>
            new(
                media.ContainerName,
                media.BlobName,
                media.Type,
                media.TranscodedBlobName,
                media.Metadata!,
                media.ModerationResult!,
                media.Thumbnails
            );

        public static DeletePostMediaResponse Map(Post post, Media deletedMedia) =>
            new(
                post.Id,
                post.Version,
                post.CreatedAt,
                post.UpdatedAt,
                Map(post.Content),
                [..post.Media.Select(Map)],
                Map(deletedMedia)
            );
    }
}
