using MassTransit;

namespace BlobService.Application.UseCases.CreateContainer
{
    internal class CreateContainerConsumer(IContainerService containerService) : IConsumer<CreateContainerRequest>
    {
        private readonly IContainerService _containerService = containerService;

        public Task Consume(ConsumeContext<CreateContainerRequest> context) =>
            _containerService.CreateAsync(
                context.Message.ContainerName,
                context.CancellationToken
            );
    }
}
