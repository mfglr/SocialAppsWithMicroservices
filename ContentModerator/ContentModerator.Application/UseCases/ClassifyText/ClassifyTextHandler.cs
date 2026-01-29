using MediatR;
using Shared.Events;

namespace ContentModerator.Application.UseCases.ClassifyText
{
    internal class ClassifyTextHandler(IModerator moderator) : IRequestHandler<ClassifyTextRequest,ModerationResult>
    {
        private readonly IModerator _moderator = moderator;

        public Task<ModerationResult> Handle(ClassifyTextRequest request, CancellationToken cancellationToken) =>
            _moderator.ClassifyTextAsync(request.Text, cancellationToken);
    }
}
