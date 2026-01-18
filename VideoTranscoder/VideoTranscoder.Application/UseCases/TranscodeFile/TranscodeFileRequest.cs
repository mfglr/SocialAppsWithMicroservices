using MediatR;
using Microsoft.AspNetCore.Http;

namespace VideoTranscoder.Application.UseCases.TranscodeFile
{
    public record TranscodeFileRequest(IFormFile File) : IRequest<byte[]>;
}
