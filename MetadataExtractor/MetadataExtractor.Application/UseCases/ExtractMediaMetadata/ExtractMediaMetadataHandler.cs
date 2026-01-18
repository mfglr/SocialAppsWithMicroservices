using MassTransit;
using MediatR;
using Shared.Events.MediaService;

namespace MetadataExtractor.Application.UseCases.ExtractMediaMetadata
{
    internal class ExtractMediaMetadataHandler(IBlobService blobService, IMetadataExtractor extractor, IPublishEndpoint publishEndpoint) : IRequestHandler<ExtractMediaMetadataRequest>
    {
        private readonly IBlobService _blobService = blobService;
        private readonly IMetadataExtractor _extractor = extractor;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(ExtractMediaMetadataRequest request, CancellationToken cancellationToken)
        {
            using var stream = await _blobService.ReadAsync(request.ContainerName, request.BlobName, cancellationToken);
            var metadata = await _extractor.ExtractAsync(stream, cancellationToken);
            await _publishEndpoint.Publish(
                new MediaMetadataExtractedEvent(
                    request.Id,
                    request.ContainerName,
                    request.BlobName,
                    request.Type,
                    metadata
                ),
                cancellationToken
            );
        }
    }
}
