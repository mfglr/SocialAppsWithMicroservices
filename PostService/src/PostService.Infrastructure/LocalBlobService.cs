using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PostService.Application;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace PostService.Infrastructure
{
    internal class LocalBlobService(IConfiguration configuration, IAccessTokenProvider accessTokenProvider) : IBlobService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IAccessTokenProvider _accessTokenProvider = accessTokenProvider;

        public async Task DeleteAsync(string containerName, IEnumerable<string> blobNames, CancellationToken cancellationToken)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["BlobService:ConnectionString"]!),
            };
            var accessToken = _accessTokenProvider.Get();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var content = JsonContent.Create(new { containerName, blobNames });
            await client.PostAsync("api/v1/blobs/delete", content, cancellationToken);
        }

        public async Task<IReadOnlyList<string>> UploadAsync(string containerName, IFormFileCollection files, CancellationToken cancellationToken)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(_configuration["BlobService:Host"]!),
            };
            var accessToken = _accessTokenProvider.Get();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            var form = new MultipartFormDataContent { { new StringContent(containerName), "containerName" } };

            List<Stream> streams = [];

            foreach (var file in files)
            {
                var stream = file.OpenReadStream();
                streams.Add(stream);
                var streamContent = new StreamContent(stream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                form.Add(streamContent, "media", "media");
            }
            var message = await client.PostAsync("api/v1/blobs/upload", form, cancellationToken);
            streams.ForEach(stream =>
            {
                stream.Close();
                stream.Dispose();
            });
            var content = await message.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<List<string>>(content)!;
        }
    }
}
