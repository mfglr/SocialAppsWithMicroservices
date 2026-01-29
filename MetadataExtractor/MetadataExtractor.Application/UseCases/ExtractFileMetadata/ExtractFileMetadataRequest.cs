using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Events;

namespace MetadataExtractor.Application.UseCases.ExtractFileMetadata
{
    public record ExtractFileMetadataRequest(IFormFile File) : IRequest<Metadata>;
}
