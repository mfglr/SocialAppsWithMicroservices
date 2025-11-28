using Shared.Objects;

namespace MediaService.Application.UseCases.SetMediaModerationResult
{
    public record SetMediaModerationResultRequest(Guid Id, ModerationResult ModerationResult);
}
