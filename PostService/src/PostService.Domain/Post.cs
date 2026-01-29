using PostService.Domain.Exceptions;

namespace PostService.Domain
{
    public class Post
    {
        public readonly static int MaxMediaCount = 5;
        public readonly static string MediaContainerName = "PostMedia";

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Guid UserId { get; private set; }
        public bool IsDeleted { get; private set; }
        public int Version { get; private set; }
        public Content? Content { get; private set; }
        public IReadOnlyList<Media> Media { get; private set; }

        public bool IsValid() => !Media.Any(x => !x.IsValid());

        public Post(Guid userId, Content? content, IReadOnlyList<Media> media)
        {
            if (media.Count <= 0)
                throw new PostMediaRequiredException();

            if (media.Count > MaxMediaCount)
                throw new PostMediaCountException();

            if (media.Any(x => x.ContainerName != MediaContainerName))
                throw new InvalidContainerName();

            Id = Guid.CreateVersion7();
            UserId = userId;
            Content = content;
            Media = media;
            CreatedAt = DateTime.UtcNow;
            Version = 1;
        }
        
        public void Delete()
        {
            if (IsDeleted)
                throw new PostNotFoundException();

            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
            Version++;
        }
        public void Restore()
        {
            if (!IsDeleted)
                throw new PostAlreadyAvailableException();

            IsDeleted = false;
            UpdatedAt = DateTime.UtcNow;
            Version++;
        }
        public void SetContentModerationResult(ModerationResult result)
        {
            Content = Content?.SetModerationResult(result);
            UpdatedAt = DateTime.UtcNow;
            Version++;
        }
        public void UpdateContent(Content content)
        {
            if (IsDeleted)
                throw new PostNotFoundException();

            Content = content;
            UpdatedAt = DateTime.UtcNow;
            Version++;
        }
        public void SetMedia(IEnumerable<Media> media)
        {
            Media = [.. media];
            UpdatedAt = DateTime.UtcNow;
            Version++;
        }
    }
}
