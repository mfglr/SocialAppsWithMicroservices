using MediatR;
using Shared.Objects;

namespace PostService.Application.UseCases.SetPostContentModerationResult
{
    public record SetPostContentModerationResultRequest(Guid Id, ModerationResult ModerationResult) : IRequest;
}
