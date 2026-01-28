using MediatR;
using Shared.Objects;

namespace MetadataExtractor.Application.UseCases.ExtractMetadata
{
    internal class ExtractMetadataHandler(IBlobService blobService, IMetadataExtractor extractor) : IRequestHandler<ExtractMetadataRequest, Metadata>
    {
        private readonly IBlobService _blobService = blobService;
        private readonly IMetadataExtractor _extractor = extractor;

        public async Task<Metadata> Handle(ExtractMetadataRequest request, CancellationToken cancellationToken)
        {
            using var stream = await _blobService.ReadAsync(request.ContainerName, request.BlobName, cancellationToken);
            return await _extractor.ExtractAsync(stream, cancellationToken);
        }
    }
}
