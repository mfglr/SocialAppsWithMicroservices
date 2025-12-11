using Shared.Objects;

namespace QueryService.Domain.CommentDomain
{
    public class CommentContent
    {
        public string Value { get; private set; } = null!;
        public ModerationResult ModerationResult {  get; private set; } = null!;

        private CommentContent() { }
        
        public CommentContent(string value, ModerationResult moderationResult)
        {
            Value = value;
            ModerationResult = moderationResult;
        }
    }
}
