namespace CommentService.Infrastructure
{
    public class AppConcurrencyException() : Exception("Conflict detected.");
}
