using BlobService.Application.UseCases.DeleteBlob;
using Shared.Events.PostService;

namespace BlobService.Workers.Consumers.DeleteBlobsOnPostDeleted
{
    internal class DeleteBlobsOnPostDeletedMapper
    {
        private static IEnumerable<string> Map(PostDeletedEvent_Media media) =>
            media.TranscodedBlobName != null
            ? [media.BlobName, media.TranscodedBlobName, .. media.Thumbnails.Select(x => x.BlobName)]
            : [media.BlobName, .. media.Thumbnails.Select(x => x.BlobName)];

        public static DeleteBlobRequest Map(PostDeletedEvent @event) =>
            new (
                @event.Media[0].ContainerName,
                @event.Media.Select(x => Map(x)).Aggregate((x, y) => [..x, .. y])
            );
    }
}
