using System.Text.Json.Serialization;

namespace Shared.Objects
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
        public bool IsDeleted { get; private set; }


        [JsonIgnore]
        public bool IsValid =>
            (Metadata?.IsValid ?? false) &&
            (ModerationResult?.IsValid ?? false) &&
            Thumbnails.Count == 2;


        [JsonConstructor]
        private Media(string containerName, string blobName, MediaType type, string? transcodedBlobName, Metadata? metadata, ModerationResult? moderationResult, IReadOnlyList<Thumbnail> thumbnails, bool isDeleted)
        {
            ContainerName = containerName;
            BlobName = blobName;
            Type = type;
            TranscodedBlobName = transcodedBlobName;
            Metadata = metadata;
            ModerationResult = moderationResult;
            Thumbnails = thumbnails;
            IsDeleted = isDeleted;
        }

        public Media(string containerName, string blobName, MediaType type)
        {
            ContainerName = containerName;
            BlobName = blobName;
            Type = type;
            Thumbnails = [];
        }

        public Media Set(
            string? transcodedBlobName,
            Metadata metadata,
            ModerationResult? moderationResult,
            IEnumerable<Thumbnail> thumbnails
        ) => 
            new(
                ContainerName,
                BlobName,
                Type,
                transcodedBlobName,
                metadata,
                moderationResult,
                [.. thumbnails],
                IsDeleted
            );

        public Media Delete() => 
            new(
                ContainerName,
                BlobName,
                Type,
                TranscodedBlobName,
                Metadata,
                ModerationResult,
                Thumbnails,
                true
            );
    }
}
