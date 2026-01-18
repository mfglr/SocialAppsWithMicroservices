using AutoMapper;
using MassTransit;
using MediaService.Domain;
using MediatR;
using Shared.Events.MediaService;

namespace MediaService.Application.UseCases.CreateMedia
{
    internal class CreateMediaHandler(IMediaRepository mediaRepository, IPublishEndpoint publishEndpoint, IMapper mapper) : IRequestHandler<CreateMediaRequest>
    {
        private readonly IMediaRepository _mediaRepository = mediaRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(CreateMediaRequest request, CancellationToken cancellationToken)
        {
            var media = request.Media
                .Select(x => new Media(request.Id, x.ContainerName, x.BlobName, x.Type))
                .ToList();

            foreach (var mediaItem in media)
                mediaItem.Create();

            await _mediaRepository.CreateRangeAsync(media, cancellationToken);

            var events = _mapper.Map<IEnumerable<Media>, IEnumerable<MediaCreatedEvent>>(media);
            await _publishEndpoint.PublishBatch(events, cancellationToken);
        }
    }
}
