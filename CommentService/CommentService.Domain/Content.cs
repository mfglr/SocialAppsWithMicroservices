using CommentService.Domain.Exceptions;
using Shared.Events;

namespace CommentService.Domain
{
    public class Content
    {
        public static readonly int MinLength = 1;
        public static readonly int MaxLength = 1024;

        public string Value { get; private set; }
        public ModerationResult? ModerationResult { get; set; }

        public Content(string value)
        {
            if (value == null || value.Length < MinLength || value.Length > MaxLength)
                throw new CommentLengthException();
            Value = value;
        }
    }
}
