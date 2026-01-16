using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using VideoTranscoder.Application;

namespace VideoTranscoder.Infrastructure
{
    internal class LocalBlobService(IConfiguration configuration, IAccessTokenProvider accessTokenProvider) : IBlobService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IAccessTokenProvider _accessTokenProvider = accessTokenProvider;

        public async Task<string> UploadAsync(Stream stream, string containerName, CancellationToken cancellationToken)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["BlobService:Host"]!)
            };
            client.DefaultRequestHeaders.Add("Authorization",$"Bearer {_accessTokenProvider.GetAccessToken()}");

            var form = new MultipartFormDataContent
            {
                { new StringContent(containerName), "containerName" },
                { new StreamContent(stream), "media", "media" }
            };

            var response = await client.PostAsync("api/v1/blobs/upload", form, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<List<string>>(content)!.First();
        }
        public async Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["BlobService:Host"]!)
            };
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessTokenProvider.GetAccessToken()}");

            var content = JsonContent.Create(new { containerName, blobNames = new[] { blobName } });
            await client.PostAsync("api/v1/blobs/delete", content, cancellationToken);
        }
        public async Task<Stream> GetAsync(string containerName, string blobName, CancellationToken cancellationToken)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["BlobService:Host"]!)
            };

            var accessToken = _accessTokenProvider.GetAccessToken();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            return await client.GetStreamAsync($"api/v1/blobs/get/{containerName}/{blobName}", cancellationToken);
        }
    }
}
