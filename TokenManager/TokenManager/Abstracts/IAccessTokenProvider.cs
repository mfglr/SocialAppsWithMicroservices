namespace TokenManager.Abstracts
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAccessTokenAsync(string clientId, string clientSecret, CancellationToken cancellationToken);
    }
}
