using Shared.Objects;

namespace MediaService.Domain
{
    public class Media
    {
        public Guid Id { get; private set; }
        public Guid OwnerId { get; private set; }
        public int Version { get; private set; }
        public string ContainerName { get; private set; }
        public string BlobName { get; private set; }
        public MediaType Type { get; private set; }
        public string? TranscodedBlobName { get; private set; }
        public Metadata? Metadata { get; private set; }
        public ModerationResult? ModerationResult { get; private set; }
        public IReadOnlyCollection<Thumbnail> Thumbnails { get; private set; }

        public bool IsPreprocessingCompleted =>
            Version == 6 ||
            (Version == 5 && Type == MediaType.Image) ||
            (Metadata != null && !Metadata.IsValid);

        public Media(Guid ownerId, string containerName, string blobName, MediaType type)
        {
            OwnerId = ownerId;
            ContainerName = containerName;
            BlobName = blobName;
            Type = type;
            Thumbnails = [];
        }

        public void Create()
        {
            Version = 1;
            Id = Guid.CreateVersion7();
        }

        public void SetModerationResult(ModerationResult moderationResult)
        {
            Version++;
            ModerationResult = moderationResult;
        }

        public void SetThumbnail(Thumbnail thumbnail)
        {
            Version++;
            Thumbnails = [.. Thumbnails, thumbnail];
        }

        public void SetTranscodedBlobName(string blobName)
        {
            Version++;
            TranscodedBlobName = blobName;
        }

        public void SetMetadata(Metadata metadata)
        {
            Version++;
            Metadata = metadata;
        }
    }
}
