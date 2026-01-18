using MediatR;

namespace MediaService.Application.UseCases.SetMediaTranscodedBlobName
{
    public record SetMediaTranscodedBlobNameRequest(Guid Id, string BlobName) : IRequest;
}
