using Shared.Objects;
using System.Text.Json.Serialization;

namespace PostService.Domain
{
    public class Post
    {
        public readonly static int MaxMediaLength = 5;
        public readonly static string MediaContainerName = "PostMedia";

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public int Version { get; private set; }
        public Content? Content { get; private set; }
        public IReadOnlyList<Media> Media { get; private set; }

        [JsonConstructor]
        private Post(Guid id, DateTime createdAt, DateTime? updatedAt, int version, Content? content, IReadOnlyList<Media> media)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Version = version;
            Content = content;
            Media = media;
        }

        public Post(Content? content, IReadOnlyList<Media> media)
        {
            if (content == null && media.Count == 0)
                throw new Exception("Post content exception.");

            if (media.Count > MaxMediaLength)
                throw new Exception("Post media exception.");

            Content = content;
            Media = media;
        }

        public void Create()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Version = 0;
        }

        public void SetContentModerationResult(ModerationResult result)
        {
            Version++;
            Content = Content?.SetModerationResult(result);
        }

        public void SetMedia(string blobName,string? transcodedBlobName, Metadata metaData, ModerationResult moderationResult, IEnumerable<Thumbnail> thumbnails)
        {
            Version++;
            Media =
                [
                    ..Media
                        .Select(
                            x => x.BlobName == blobName
                                ? x.Set(transcodedBlobName, metaData, moderationResult, thumbnails)
                                : x
                        )
                ];
        }

    }
}
