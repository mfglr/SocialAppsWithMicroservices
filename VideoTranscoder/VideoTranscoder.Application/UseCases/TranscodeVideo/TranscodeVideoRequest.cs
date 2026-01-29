using MediatR;

namespace VideoTranscoder.Application.UseCases.TranscodeVideo
{
    public record TranscodeVideoRequest(string ContainerName, string BlobName) : IRequest<TranscodeVideoResponse>;
}
