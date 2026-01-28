using Shared.Objects;

namespace UserService.Application.UseCases.SetMediaModerationResult
{
    public record SetMediaModerationResultRequest(Guid Id, string BlobName, ModerationResult ModerationResult) : MediatR.IRequest;
}
