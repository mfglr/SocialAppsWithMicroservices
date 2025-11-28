using Azure.AI.ContentSafety;
using ContentModerator.Application;
using Shared.Objects;

namespace ContentModerator.Infrastructure.AzureAIContentModeration
{
    internal class AzureAIContentModerator(ContentSafetyClient client, ImageResultMapper imageResultMapper, TextResultMapper textResultMapper) : IModerator
    {
        private readonly ContentSafetyClient _client = client;
        private readonly ImageResultMapper _imageResultMapper = imageResultMapper;
        private readonly TextResultMapper _textResultMapper = textResultMapper;

        public async Task<ModerationResult> ClassifyImageAsync(string inputPath, CancellationToken cancellationToken)
        {
            using var stream = File.OpenRead(inputPath);
            var bytes = new byte[stream.Length];
            await stream.ReadExactlyAsync(bytes, cancellationToken);
            var response = await _client.AnalyzeImageAsync(new BinaryData(bytes), cancellationToken);
            return _imageResultMapper.Map(response.Value);
        }

        public async Task<ModerationResult> ClassifyTextAsync(string text, CancellationToken cancellationToken)
        {
            var response = await _client.AnalyzeTextAsync(text, cancellationToken);
            return _textResultMapper.Map(response.Value);
        }
    }
}
