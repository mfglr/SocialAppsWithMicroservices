using MediatR;

namespace PostService.Application.UseCases.DeletePostMedia
{
    public record DeletePostMediaRequest(Guid Id, string BlobName) : IRequest;
}
