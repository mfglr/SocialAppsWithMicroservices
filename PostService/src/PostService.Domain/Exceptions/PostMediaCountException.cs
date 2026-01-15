namespace PostService.Domain.Exceptions
{
    public class PostMediaCountException() : Exception($"You can upload up to {Post.MaxMediaCount} media per post!");
}
