using MediatR;
using Shared.Events;

namespace ContentModerator.Application.UseCases.ClassifyText
{
    public record ClassifyTextRequest(string Text) : IRequest<ModerationResult>;
}
