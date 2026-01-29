using Newtonsoft.Json;

namespace PostMedia.Domain
{
    [GenerateSerializer]
    [Alias("PostMedia.Domain.Post")]
    public class Post
    {
        [JsonConstructor]
        private Post(Guid id, List<Media> media)
        {
            Id = id;
            _media = media;
        }

        public static readonly string MediaContainerName = "PostMedia";

        [Id(0)]
        public Guid Id { get; private set; }
        [Id(1)]
        private readonly List<Media> _media = [];
        public IReadOnlyList<Media> Media => _media;

        public Post(Guid id, IEnumerable<Media> media)
        {
            Id = id;
            _media.AddRange(media);
        }
            
        public bool IsPreprocessingCompleted() => !_media.Any(x => !x.IsPreprocessingCompleted());

        public void SetMetadata(string blobName, Metadata metadata)
        {
            var media = _media.FirstOrDefault(x => x.BlobName == blobName) ?? throw new MediaNotFoundException();
            media.SetMetadata(metadata);
        }
        public void SetModerationResult(string blobName, ModerationResult moderationResult)
        {
            var media = _media.FirstOrDefault(x => x.BlobName == blobName) ?? throw new MediaNotFoundException();
            media.SetModerationResult(moderationResult);
        }
        public void AddThumbnail(string blobName, Thumbnail thumbnail)
        {
            var media = _media.FirstOrDefault(x => x.BlobName == blobName) ?? throw new MediaNotFoundException();
            media.AddThumbnail(thumbnail);
        }
        public void SetTranscodedBlobName(string blobName, string transcodedBlobName)
        {
            var media = _media.FirstOrDefault(x => x.BlobName == blobName) ?? throw new MediaNotFoundException();
            media.SetTranscodedBlobName(transcodedBlobName);
        }
    }
}
