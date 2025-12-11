namespace CommentService.Domain.Exceptions
{
    internal class CommentLengthException() : Exception($"A comment must be between {Content.MinLength} and {Content.MaxLength} characters in length.");
}
