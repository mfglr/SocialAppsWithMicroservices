using ContentModerator.Application.UseCases.ClassifyImage;
using MassTransit;
using MediatR;
using Shared.Events.UserService;

namespace ContentModerator.Worker.Consumers.UserDomain
{
    internal class ClassifyImage_OnUserMediaCreated_ContentModerator(ISender sender, IPublishEndpoint publishEndpoint) : IConsumer<UserMediaCreatedEvent>
    {
        public async Task Consume(ConsumeContext<UserMediaCreatedEvent> context)
        {
            var result = await sender.Send(
                new ClassifyImageRequest(context.Message.Media.ContainerName, context.Message.Media.BlobName),
                context.CancellationToken
            );
            await publishEndpoint
                .Publish(
                    new UserMediaClassfiedEvent(
                        context.Message.Id,
                        context.Message.Media.BlobName,
                        result
                    ),
                    context.CancellationToken
                );
        }
    }
}
