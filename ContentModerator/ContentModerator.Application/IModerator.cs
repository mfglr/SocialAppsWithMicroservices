using Shared.Objects;

namespace ContentModerator.Application
{
    public interface IModerator
    {
        Task<ModerationResult> ClassifyImageAsync(string inputPath, CancellationToken cancellationToken);
        Task<ModerationResult> ClassifyTextAsync(string text, CancellationToken cancellationToken);
    }
}
