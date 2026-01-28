using MediatR;

namespace VideoTranscoder.Application.UseCases.TranscodeVideo
{
    public record TranscodeVideoRequest(Guid Id, string ContainerName, string BlobName) : IRequest<TranscodeVideoResponse>;
}
