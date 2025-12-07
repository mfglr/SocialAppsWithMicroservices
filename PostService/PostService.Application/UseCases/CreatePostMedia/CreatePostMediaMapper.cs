using PostService.Domain;

namespace PostService.Application.UseCases.CreatePostMedia
{
    internal static class CreatePostMediaMapper
    {
        public static CreatePostMediaResponse ToCreatePostMediaResponse(Guid id, IEnumerable<Media> media) =>
            new(
                id,
                media.Select(x => new CreatePostMediaResponse_Media(
                    x.ContainerName,
                    x.BlobName,
                    x.Type
                ))
            );
    }
}
