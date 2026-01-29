using Shared.Events;

namespace PostMedia.Application.UseCases.SetPostMediaModerationResult
{
    public record SetPostMediaModerationResultRequest(
        Guid Id,
        string BlobName,
        ModerationResult ModerationResult
    ) : MediatR.IRequest;
}
