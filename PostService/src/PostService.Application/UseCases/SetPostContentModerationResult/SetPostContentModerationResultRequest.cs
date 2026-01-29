using MediatR;
using Shared.Events;

namespace PostService.Application.UseCases.SetPostContentModerationResult
{
    public record SetPostContentModerationResultRequest(Guid Id, ModerationResult ModerationResult) : IRequest;
}
