using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Events.UserService;
using UserService.Domain;

namespace UserService.Application.UseCases.SetMediaModerationResult
{
    internal class SetMediaModerationResultHandler(IMapper mapper, IPublishEndpoint publishEndpoint, IGrainFactory grainFactory) : IRequestHandler<SetMediaModerationResultRequest>
    {
        public async Task Handle(SetMediaModerationResultRequest request, CancellationToken cancellationToken)
        {
            var userGrain = grainFactory.GetGrain<IUserGrain>(request.Id);
            var moderationResult = mapper.Map<Shared.Events.ModerationResult, ModerationResult>(request.ModerationResult);
            await userGrain.SetMediaModerationResult(request.BlobName, moderationResult);

            var user = await userGrain.Get();
            if (user.IsPreprocessingCompleted())
            {
                var @event = mapper.Map<User, UserUpdatedEvent>(user);
                await publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
