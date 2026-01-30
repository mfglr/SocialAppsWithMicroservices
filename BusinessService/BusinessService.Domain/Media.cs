namespace BusinessService.Domain
{
    public class Media
    {
        public string ContainerName { get; private set; }
        public string BlobName { get; private set; }
        public MediaType Type { get; private set; }
        public Metadata? Metadata { get; private set; }
        public ModerationResult? ModerationResult { get; private set; }
        public List<Thumbnail> Thumbnails { get; private set; }
        public bool IsDeleted { get; private set; }

        public Media(string blobName)
        {
            ContainerName = Business.ContainerName;
            BlobName = blobName;
            Thumbnails = [];
            Type = MediaType.Image;
            IsDeleted = false;
        }

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
