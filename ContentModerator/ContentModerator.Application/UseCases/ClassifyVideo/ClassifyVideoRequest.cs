using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Events;

namespace ContentModerator.Application.UseCases.ClassifyVideo
{
    public record ClassifyVideoRequest(IFormFile File) : IRequest<ModerationResult>;
}
