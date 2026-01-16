namespace TokenManager.Abstracts
{
    public interface IAccessTokenCache
    {
        void Set(string clientId, string accessToken);
    }
}
