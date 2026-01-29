namespace PostService.Domain
{
    public class Media
    {
        public string ContainerName { get; private set; }
        public string BlobName { get; private set; }
        public MediaType Type { get; private set; }
        public Metadata? Metadata { get; private set; }
        public ModerationResult? ModerationResult { get; private set; }
        public IReadOnlyList<Thumbnail> Thumbnails { get; private set; }
        public string? TranscodedBlobName { get; private set; }

        public Media(string blobName, MediaType type)
        {
            ContainerName = Post.MediaContainerName;
            BlobName = blobName;
            Type = type;
            Thumbnails = [];
        }

        public bool IsValid() =>
            Metadata != null && Metadata.IsValid() &&
            ModerationResult != null && ModerationResult.IsValid() &&
            Thumbnails.Count == 3 &&
            (Type == MediaType.Image || TranscodedBlobName != null);
            
    }
}
