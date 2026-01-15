namespace PostService.Infrastructure
{
    public class AppConcurrencyException() : Exception("Conflict detected.");
}
