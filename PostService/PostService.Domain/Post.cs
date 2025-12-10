using PostService.Domain.Exceptions;
using Shared.Objects;
using System.Text.Json.Serialization;

namespace PostService.Domain
{
    public class Post
    {
        public readonly static int MaxMediaCount = 5;
        public readonly static string MediaContainerName = "PostMedia";

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
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
            if (media.Count <= 0)
                throw new PostMediaRequiredException();

            if (media.Count > MaxMediaCount)
                throw new PostMediaCountException();

            Content = content;
            Media = media;
        }

        public void Create()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Version = 0;
            IsDeleted = false;
        }
        public void Update()
        {
            Version++;
            UpdatedAt = DateTime.UtcNow;
        }
        public void Delete()
        {
            IsDeleted = true;
            Update();
        }

        public void UpdateContent(Content content)
        {
            Content = content;
            Update();
        }

        public void SetContentModerationResult(ModerationResult result)
        {
            Content = Content?.SetModerationResult(result);
            Update();
        }

        public void SetMedia(string blobName,string? transcodedBlobName, Metadata metaData, ModerationResult? moderationResult, IEnumerable<Thumbnail> thumbnails)
        {
            var media =
                Media.FirstOrDefault(x => x.BlobName == blobName) ??
                throw new PostMediaNotFoundException();

            Media =
                [
                    ..Media
                        .Select(
                            x => x == media
                                ? x.Set(transcodedBlobName, metaData, moderationResult, thumbnails)
                                : x
                        )
                ];
            Update();
        }

        public void AddMedia(IReadOnlyList<Media> media, string? offset)
        {
            int index;
            if (offset == null)
                index = -1;
            else
            {
                var item = Media.Index().First(x => x.Item.BlobName == offset);
                if (item.Item == null)
                    throw new Exception("Ofset not found");
                index = item.Index;
            }

            if (Media.Where(x => !x.IsDeleted).Count() + media.Count > MaxMediaCount)
                throw new PostMediaCountException();

            var list = Media.ToList();
            Media = [.. list[0..(index + 1)], .. media, .. list[(index + 1)..]];
            Update();
        }

        public void DeleMedia(string blobName)
        {
            var media =
                Media.FirstOrDefault(x => x.BlobName == blobName) ??
                throw new PostMediaNotFoundException();

            if (media.IsDeleted)
                throw new PostMediaNotFoundException();

            if (Media.Where(x => !x.IsDeleted).Count() <= 1)
                throw new PostMediaRequiredException();

            Media = [.. Media.Select(x => x == media ? x.Delete() : x)];
            Update();
        }

        
    }
}
