namespace PostService.Infrastructure.MongoDb
{
    public class AppConcurrencyException() : Exception("Conflict detected.");
}
