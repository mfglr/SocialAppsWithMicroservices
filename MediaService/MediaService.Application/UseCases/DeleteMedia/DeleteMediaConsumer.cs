using MassTransit;
using MediaService.Domain;

namespace MediaService.Application.UseCases.DeleteMedia
{
    internal class DeleteMediaConsumer(IMediaRepository repository) : IConsumer<DeleteMediaRequest>
    {
        private readonly IMediaRepository _repository = repository;

        public Task Consume(ConsumeContext<DeleteMediaRequest> context) =>
            _repository.DeleteAsync(context.Message.Id, context.CancellationToken);
    }
}
