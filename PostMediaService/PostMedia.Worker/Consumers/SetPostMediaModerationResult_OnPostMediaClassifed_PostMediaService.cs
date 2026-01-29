using MassTransit;
using MediatR;
using PostMedia.Application.UseCases.SetPostMediaModerationResult;
using Shared.Events.PostMediaService;

namespace PostMedia.Worker.Consumers
{
    internal class SetPostMediaModerationResult_OnPostMediaClassifed_PostMediaService(ISender sender) : IConsumer<PostMediaClassifiedEvent>
    {
        public Task Consume(ConsumeContext<PostMediaClassifiedEvent> context) =>
            sender
                .Send(
                    new SetPostMediaModerationResultRequest(
                        context.Message.Id,
                        context.Message.BlobName,
                        context.Message.ModerationResult
                    ),
                    context.CancellationToken
                );
    }
}
