using Shared.Objects;
using System.Text.Json.Serialization;

namespace PostService.Domain
{
    public class Media
    {
        public string ContainerName { get; private set; }
        public string BlobName { get; private set; }
        public MediaType Type { get; private set; }
        public string? TranscodedBlobName { get; private set; }
        public Metadata? Metadata { get; private set; }
        public ModerationResult? ModerationResult { get; private set; }
        public IReadOnlyList<Thumbnail> Thumbnails { get; private set; }

        [JsonConstructor]
        private Media(string containerName, string blobName, MediaType type, string? transcodedBlobName, Metadata? metadata, ModerationResult? moderationResult, IReadOnlyList<Thumbnail> thumbnails)
        {
            ContainerName = containerName;
            BlobName = blobName;
            Type = type;
            TranscodedBlobName = transcodedBlobName;
            Metadata = metadata;
            ModerationResult = moderationResult;
            Thumbnails = thumbnails;
        }

        public Media(string blobName, MediaType type)
        {
            ContainerName = Post.MediaContainerName;
            BlobName = blobName;
            Type = type;
            Thumbnails = [];
        }

        public Media Set(string? transcodedBlobName, Metadata metaData, ModerationResult moderationResult, IEnumerable<Thumbnail> thumbnails) => new(ContainerName, BlobName, Type, transcodedBlobName, metaData, moderationResult, [.. thumbnails]);
    }
}
