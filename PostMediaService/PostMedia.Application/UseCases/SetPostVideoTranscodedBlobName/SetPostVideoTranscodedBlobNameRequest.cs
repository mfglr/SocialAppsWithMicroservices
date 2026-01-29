namespace PostMedia.Application.UseCases.SetPostVideoTranscodedBlobName
{
    public record SetPostVideoTranscodedBlobNameRequest(Guid Id, string BlobName, string TranscodedBlobName) : MediatR.IRequest;
}
