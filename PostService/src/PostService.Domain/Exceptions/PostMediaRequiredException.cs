namespace PostService.Domain.Exceptions
{
    public class PostMediaRequiredException() : Exception("A post must have at least one media item.");
}
