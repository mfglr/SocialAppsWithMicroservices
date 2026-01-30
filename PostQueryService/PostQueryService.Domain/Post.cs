namespace PostQueryService.Domain
{
    public class Post
    {

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public Content? Content { get; private set; }
        public IReadOnlyList<Media> Media { get; private set; }
        public int PostVersion { get; private set; }

        public Guid UserId { get; private set; }
        public string UserName { get; private set; }
        public string? Name { get; private set; }
        public Media? ProfilePhoto { get; private set; }
        public int UserVersion { get; private set; }

        public int ConcurrencyToken { get; private set; }

        public Post(
            Guid id,
            DateTime createdAt,
            DateTime? updatedAt,
            bool isDeleted,
            int postVersion,
            Guid userId,
            Content? content,
            IEnumerable<Media> media
        )
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsDeleted = isDeleted;
            PostVersion = postVersion;
            UserId = userId;
            Content = content;
            Media = [.. media];
            ConcurrencyToken = 1;
        }

        public void SetPost(DateTime? updatedAt, bool isDeleted, int postVersion, Content? content, IEnumerable<Media> media)
        {
            if (postVersion <= PostVersion)
                return;

            UpdatedAt = updatedAt;
            IsDeleted = isDeleted;
            PostVersion = postVersion;
            Content = content;
            Media = [..media];
            ConcurrencyToken++;
        }

        public void SetUser(string userName, string? name, Media? profilePhoto, int userVersion)
        {
            if (userVersion <= UserVersion)
                return;

            UserName = userName;
            Name = name;
            ProfilePhoto = profilePhoto;
            UserVersion = userVersion;
            ConcurrencyToken++;
        }
    }
}
