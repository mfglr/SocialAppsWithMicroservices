namespace CommentService.Domain.Exceptions
{
    public class ContentRequiredException() : Exception("A comment must contain content.");
}
