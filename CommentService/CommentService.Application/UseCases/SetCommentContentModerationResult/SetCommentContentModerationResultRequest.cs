using MediatR;
using Shared.Events;

namespace CommentService.Application.UseCases.SetCommentContentModerationResult
{
    public record SetCommentContentModerationResultRequest(Guid Id, ModerationResult ModerationResult) : IRequest;
}
