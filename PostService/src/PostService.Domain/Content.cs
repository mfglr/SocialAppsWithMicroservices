using Shared.Objects;

namespace PostService.Domain
{
    public class Content
    {
        public readonly static int MinLength = 2;
        public readonly static int MaxLength = 1024;
        public string Value { get; private set; }
        public ModerationResult? ModerationResult { get; private set; }

        public bool IsValidVersion => ModerationResult != null;

        private Content(string value, ModerationResult? moderationResult)
        {
            Value = value;
            ModerationResult = moderationResult;
        }

        public Content(string value)
        {
            if (value == null || value.Length < MinLength || value.Length > MaxLength)
                throw new Exception("Content exception");
            Value = value;
            ModerationResult = null;
        }

        public Content SetModerationResult(ModerationResult result) => new(Value, result);
    }
}
