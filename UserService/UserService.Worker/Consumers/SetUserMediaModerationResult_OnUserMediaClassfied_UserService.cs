using MassTransit;
using MediatR;
using Shared.Events.UserService;
using UserService.Application.UseCases.SetMediaModerationResult;

namespace UserService.Worker.Consumers
{
    internal class SetUserMediaModerationResult_OnUserMediaClassfied_UserService(ISender sender) : IConsumer<UserMediaClassfiedEvent>
    {
        public Task Consume(ConsumeContext<UserMediaClassfiedEvent> context) =>
            sender.Send(
                new SetMediaModerationResultRequest(context.Message.Id, context.Message.BlobName, context.Message.ModerationResult),
                context.CancellationToken
            );
    }
}
