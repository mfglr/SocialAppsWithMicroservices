namespace QueryService.Domain.CommentDomain
{
    public class Comment
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public byte[] RowVerstion { get; private set; } = null!;
        public int Version { get; private set; }
        public Guid UserId { get; private set; }
        public Guid PostId { get; private set; }
        public CommentContent Content { get; private set; } = null!;

        private Comment() { }

        public Comment(Guid id, DateTime createdAt, DateTime? updatedAt, int version, Guid userId, Guid postId, CommentContent content)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Version = version;
            UserId = userId;
            PostId = postId;
            Content = content;
        }

        public void Set(DateTime? updatedAt, int version, CommentContent content)
        {
            UpdatedAt = updatedAt;
            Version = version;
            Content = content;
        }
    }
}
