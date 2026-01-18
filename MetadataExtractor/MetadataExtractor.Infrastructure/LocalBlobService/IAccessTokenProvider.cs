namespace MetadataExtractor.Infrastructure.LocalBlobService
{
    public interface IAccessTokenProvider
    {
        string GetAccessToken();
    }
}
