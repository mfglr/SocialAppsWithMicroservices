using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PostService.Application;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PostService.Infrastructure
{
    internal class LocalBlobService(IConfiguration configuration) : IBlobService
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task DeleteAsync(string containerName, IEnumerable<string> blobNames, CancellationToken cancellationToken)
        {
            var baseAddress = new Uri(_configuration["BlobService:ConnectionString"]!);
            var address = $"{baseAddress}/delete";
            var client = new HttpClient();
            var content = new StringContent(JsonSerializer.Serialize(new { containerName, blobNames }), Encoding.UTF8, "application/json");
            await client.PostAsync(address, content, cancellationToken);
        }

        public async Task<IReadOnlyList<string>> UploadAsync(string containerName, IFormFileCollection files, CancellationToken cancellationToken)
        {
            var baseAddress = new Uri(_configuration["BlobService:ConnectionString"]!);
            var address = $"{baseAddress}/upload";
            var client = new HttpClient();
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
            var message = await client.PostAsync(address, form, cancellationToken);
            streams.ForEach(stream => {
                stream.Close();
                stream.Dispose();
            });
            var content = await message.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<List<string>>(content)!;
        }

        public async Task<string> UploadAsync(string containerName, IFormFile file, CancellationToken cancellationToken)
        {
            var baseAddress = new Uri(_configuration["BlobService:ConnectionString"]!);
            var address = $"{baseAddress}/upload";
            var client = new HttpClient();
            var form = new MultipartFormDataContent { { new StringContent(containerName), "containerName" } };

            List<Stream> streams = [];

            var stream = file.OpenReadStream();
            streams.Add(stream);
            var streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            form.Add(streamContent, "media", "media");

            var message = await client.PostAsync(address, form, cancellationToken);
           
            var content = await message.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<List<string>>(content)!.First();
        }
    }
}
