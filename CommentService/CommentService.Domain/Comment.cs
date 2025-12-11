using CommentService.Domain.Exceptions;
using Shared.Objects;

namespace CommentService.Domain
{
    public class Comment
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public int Version { get; private set; }
        public Guid UserId { get; private set; }
        public Guid PostId  { get; private set; }
        public Content Content { get; private set; }

        public Comment(Guid userId, Guid postId,  Content content)
        {
            UserId = userId;
            PostId = postId;
            Content = content ?? throw new ContentRequiredException();
        }

        public void Create()
        {
            Id = Guid.NewGuid();
            Version = 0;
            CreatedAt = DateTime.UtcNow;
        }
        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
            Version++;
        }
        public void Delete()
        {
            IsDeleted = true;
            Update();
        }

        public void SetModerationResult(ModerationResult result)
        {
            Content.ModerationResult = result;
            Update();
        }
        public void UpdateContent(Content content)
        {
            Content = content;
            Update();
        }
    }
}
