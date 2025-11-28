using Shared.Objects;

namespace PostService.Application.UseCases.SetContentModerationResult
{
    public record SetContentModerationResultRequest(Guid Id, ModerationResult ModerationResult);
}
