using System;
using static System.Net.Mime.MediaTypeNames;

namespace QueryService.Domain.PostDomain
{
    public class Post
    {
        public Guid Id { get; private set; }
        public byte[] RowVersion { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public int Version { get; private set; }
        public Content? Content { get; private set; }
        public List<Media> Media { get; private set; } = [];

        public bool IsValidVersion => !Media.Any(x => !x.IsValidVersion);

        private Post() { }

        public Post(Guid id,DateTime createdAt, DateTime? updatedAt, int version, Content? content, IEnumerable<Media> media)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Version = version;
            Content = content;
            Media = [.. media];
        }

        public void Set(int version, DateTime? updatedAt, Content? content, IEnumerable<Media> media)
        {
            UpdatedAt = updatedAt;
            Version = version;
            Content = content;
            Media = [..media];
        }
    }
}
