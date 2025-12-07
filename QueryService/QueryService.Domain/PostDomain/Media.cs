using Shared.Objects;

namespace QueryService.Domain.PostDomain
{
    public class Media
    {
        public string ContainerName { get; private set; } = null!;
        public string BlobName { get; private set; } = null!;
        public MediaType Type { get; private set; }
        public string? TranscodedBlobName { get; private set; }
        public Metadata Metadata { get; private set; } = null!;
        public ModerationResult? ModerationResult { get; private set; }
        public List<Thumbnail> Thumbnails { get; private set; } = null!;

        public bool IsValidVersion => ModerationResult != null;

        private Media() { }

        public Media(string containerName, string blobName, MediaType type, string? transcodedBlobName, Metadata metadata, ModerationResult? moderationResult, IEnumerable<Thumbnail> thumbnails)
        {
            ContainerName = containerName;
            BlobName = blobName;
            Type = type;
            TranscodedBlobName = transcodedBlobName;
            Metadata = metadata;
            ModerationResult = moderationResult;
            Thumbnails = [..thumbnails];
        }
    }
}
