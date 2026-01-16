using ContentModerator.Application;
using Microsoft.Extensions.Configuration;

namespace ContentModerator.Infrastructure
{
    internal class LocalBlobService(IConfiguration configuration, IAccessTokenProvider accessTokenProvider) : IBlobService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IAccessTokenProvider _accessTokenProvider = accessTokenProvider;

        public async Task<Stream> ReadAsync(string containerName, string blobName, CancellationToken cancellationToken)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["BlobService:Host"]!)
            };
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessTokenProvider.GetAccessToken()}");

            return await client.GetStreamAsync($"api/v1/blobs/get/{containerName}/{blobName}", cancellationToken);
        }
    }
}
