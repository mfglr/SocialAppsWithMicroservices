using Shared.Objects;

namespace QueryService.Domain.PostDomain
{
    public class PostContent
    {
        public string Value { get; private set; } = null!;
        public ModerationResult ModerationResult { get; private set; } = null!;

        public PostContent(string value, ModerationResult moderationResult)
        {
            Value = value;
            ModerationResult = moderationResult;
        }

        private PostContent() { }
    }
}
