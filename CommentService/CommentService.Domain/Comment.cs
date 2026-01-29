using CommentService.Domain.Exceptions;
using Shared.Events;

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
        public Guid? ParentId { get; private set; }
        public Guid? RepliedId { get; private set; }
        public Content Content { get; private set; } = null!;

        private Comment() { }

        public Comment(Guid userId, Guid postId, Guid? parentId, Guid? repliedId, Content content)
        {
            UserId = userId;
            PostId = postId;
            ParentId = parentId;
            RepliedId = repliedId ?? parentId;
            Content = content ?? throw new ContentRequiredException();
        }

        internal void Create()
        {
            Version = 1;
            Id = Guid.CreateVersion7();
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
        public void Restore()
        {
            if(!IsDeleted)
                throw new CommentAlreadyAvailableException();

            IsDeleted = false;
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
