using Newtonsoft.Json;

namespace PostMedia.Domain
{
    [GenerateSerializer]
    [Alias("PostMedia.Domain.Media")]
    public class Media
    {
        [JsonConstructor]
        public Media(string containerName, string blobName, MediaType type, Metadata? metadata, ModerationResult? moderationResult, List<Thumbnail> thumbnails, string? transcodedBlobName)
        {
            ContainerName = containerName;
            BlobName = blobName;
            Type = type;
            Metadata = metadata;
            ModerationResult = moderationResult;
            _thumbnails = thumbnails;
            TranscodedBlobName = transcodedBlobName;
        }

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
        private readonly List<Thumbnail> _thumbnails = [];
        public IReadOnlyList<Thumbnail> Thumbnails => _thumbnails;
        [Id(6)]
        public string? TranscodedBlobName { get; private set; }

        public bool IsPreprocessingCompleted() =>
            Metadata != null &&
            (
                !Metadata.IsValid() ||
                (
                    ModerationResult != null &&
                    (
                        !ModerationResult.IsValid() ||
                        (
                            Thumbnails.Count == 3 &&
                            (
                                Type == MediaType.Image ||
                                TranscodedBlobName != null
                            )
                        )
                    )
                )
            );

        public Media(string blobName)
        {
            ContainerName = Post.MediaContainerName;
            BlobName = blobName;
            Type = MediaType.Image;
        }

        public void SetMetadata(Metadata metadata) => Metadata = metadata;
        public void SetModerationResult(ModerationResult moderationResult) => ModerationResult = moderationResult;
        public void AddThumbnail(Thumbnail thumbnail) => _thumbnails.Add(thumbnail);
        public void SetTranscodedBlobName(string transcodedBlobName) => TranscodedBlobName = transcodedBlobName;
    }
}
