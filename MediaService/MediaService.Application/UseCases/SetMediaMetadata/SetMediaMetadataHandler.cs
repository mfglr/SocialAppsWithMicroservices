using AutoMapper;
using MassTransit;
using MediaService.Domain;
using MediatR;
using Shared.Events.MediaService;

namespace MediaService.Application.UseCases.SetMediaMetadata
{
    internal class SetMediaMetadataHandler(IMediaRepository mediaRepository, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<SetMediaMetadataRequest>
    {
        private readonly IMediaRepository _mediaRepository = mediaRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(SetMediaMetadataRequest request, CancellationToken cancellationToken)
        {
            var media = (await _mediaRepository.GetByIdAsync(request.Id, cancellationToken))!;
            media.SetMetadata(request.Metadata);
            await _mediaRepository.UpdateAsync(media, cancellationToken);

            if (media.IsPreprocessingCompleted)
            {
                var @event = _mapper.Map<Media, MediaPreprocessingCompletedEvent>(media);
                await _publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
