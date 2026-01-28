using MassTransit;
using MediatR;
using Shared.Objects;

namespace MetadataExtractor.Application.UseCases.ExtractMediaMetadata
{
    internal class ExtractMediaMetadataHandler(IBlobService blobService, IMetadataExtractor extractor) : IRequestHandler<ExtractMediaMetadataRequest,Metadata>
    {
        private readonly IBlobService _blobService = blobService;
        private readonly IMetadataExtractor _extractor = extractor;

        public async Task<Metadata> Handle(ExtractMediaMetadataRequest request, CancellationToken cancellationToken)
        {
            using var stream = await _blobService.ReadAsync(request.ContainerName, request.BlobName, cancellationToken);
            return await _extractor.ExtractAsync(stream, cancellationToken);
        }
    }
}
