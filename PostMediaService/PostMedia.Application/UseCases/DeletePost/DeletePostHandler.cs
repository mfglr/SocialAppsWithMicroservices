using MediatR;
using PostMedia.Domain;

namespace PostMedia.Application.UseCases.DeletePost
{
    internal class DeletePostHandler(IGrainFactory grainFactory) : IRequestHandler<DeletePostRequest>
    {
        public Task Handle(DeletePostRequest request, CancellationToken cancellationToken)
        {
            var grain = grainFactory.GetGrain<IPostGrain>(request.Id);
            return grain.Delete();
        }
    }
}
