namespace PostService.Application.Exceptions
{
    public class UnauthorizedOperationException()
        : Exception("Forbidden. Insufficient permissions to perform this operation.");
}
