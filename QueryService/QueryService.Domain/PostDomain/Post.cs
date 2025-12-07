using System.Text.Json.Serialization;

namespace QueryService.Domain.PostDomain
{
    public class Post
    {
        public Guid Id { get; private set; }
        public int Version { get; private set; }
        public byte[] RowVersion { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Content? Content { get; private set; }
        public List<Media> Media { get; private set; } = [];
        
        public bool IsValidVersion => !Media.Any(x => !x.IsValidVersion);

        private Post() { }

        public Post(Guid id, int version, DateTime createdAt, DateTime? updatedAt, Content? content, IEnumerable<Media> media)
        {
            Id = id;
            Version = version;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Content = content;
            Media = [.. media];
        }

        public void Set(Post next)
        {
            if (next.Version <= Version)
                return;
            Version = next.Version;
            UpdatedAt = next.UpdatedAt;
            Content = next.Content;
            Media = [..next.Media];
        }

    }
}
