using MassTransit;
using MediatR;
using Shared.Events.UserService;
using UserService.Application.UseCases.AddMediaThumbnail;

namespace UserService.Worker.Consumers
{
    internal class AddUserMediaThumbnail_OnUserMediaThumbnailGenerated_UserService(ISender sender) : IConsumer<UserMediaThumbnailGeneratedEvent>
    {
        public Task Consume(ConsumeContext<UserMediaThumbnailGeneratedEvent> context) =>
            sender
                .Send(
                    new AddMediaThumbnailRequest(context.Message.Id, context.Message.BlobName, context.Message.Thumbnail),
                    context.CancellationToken
                );
    }
}
