using MediaService.Domain;
using MediatR;

namespace MediaService.Application.UseCases.DeleteMedia
{
    internal class DeleteMediaHandler(IMediaRepository repository) : IRequestHandler<DeleteMediaRequest>
    {
        private readonly IMediaRepository _repository = repository;
        
        public Task Handle(DeleteMediaRequest request, CancellationToken cancellationToken) =>
            _repository.DeleteAsync(request.Id, cancellationToken);
    }
}
