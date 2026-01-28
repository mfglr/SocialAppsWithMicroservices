using Newtonsoft.Json;

namespace UserService.Domain
{
    [GenerateSerializer]
    [Alias("UserService.Domain.Media")]
    public class Media
    {
        [Id(0)]
        public string ContainerName { get; private set; }
        [Id(1)]
        public string BlobName { get; private set; }
        [Id(2)]
        public MediaType Type { get; private set; }
        [Id(3)]
        public Metadata? Metadata { get; private set; }
        [Id(4)]
        public ModerationResult? ModerationResult { get; private set; }
        [Id(5)]
        public List<Thumbnail> Thumbnails { get; private set; }
        [Id(6)]
        public bool IsDeleted { get; private set; }

        public Media(string blobName)
        {
            ContainerName = User.MediaContainerName;
            BlobName = blobName;
            Thumbnails = [];
            Type = MediaType.Image;
            IsDeleted = false;
        }

        [JsonConstructor]
        private Media(string containerName, string blobName, MediaType type,  Metadata? metadata, ModerationResult? moderationResult, List<Thumbnail> thumbnails, bool isDeleted)
        {
            ContainerName = containerName;
            BlobName = blobName;
            Type = type;
            Metadata = metadata;
            ModerationResult = moderationResult;
            Thumbnails = thumbnails;
            IsDeleted = isDeleted;
        }

        public bool IsPreprocessingCompleted() =>
            ModerationResult != null &&
            Thumbnails.Count == 4 &&
            Metadata != null;

        public void SetMetadata(Metadata metadata) => Metadata = metadata;
        public void SetModerationResult(ModerationResult moderationResult) => ModerationResult = moderationResult;
        public void AddThumbnail(Thumbnail thumbnail) => Thumbnails.Add(thumbnail);
        public void Delete() => IsDeleted = true;
    }
}
