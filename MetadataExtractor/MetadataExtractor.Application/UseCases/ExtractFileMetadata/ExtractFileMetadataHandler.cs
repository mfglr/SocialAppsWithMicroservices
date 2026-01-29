using MediatR;
using Shared.Events;

namespace MetadataExtractor.Application.UseCases.ExtractFileMetadata
{
    internal class ExtractFileMetadataHandler(IMetadataExtractor metadataExtractor) : IRequestHandler<ExtractFileMetadataRequest,Metadata>
    {
        private readonly IMetadataExtractor _metadataExtractor = metadataExtractor;

        public async Task<Metadata> Handle(ExtractFileMetadataRequest request, CancellationToken cancellationToken)
        {
            using var stream = request.File.OpenReadStream();
            return await _metadataExtractor.ExtractAsync(stream, cancellationToken);
        }
    }
}
