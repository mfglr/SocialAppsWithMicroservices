using AutoMapper;
using MassTransit;
using MediaService.Domain;
using MediatR;
using Shared.Events.MediaService;

namespace MediaService.Application.UseCases.SetMediaModerationResult
{
    internal class SetMediaModerationResultHandler(IMediaRepository mediaRepository, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<SetMediaModerationResultRequest>
    {
        private readonly IMediaRepository _mediaRepository = mediaRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(SetMediaModerationResultRequest request, CancellationToken cancellationToken)
        {
            var media = (await _mediaRepository.GetByIdAsync(request.Id, cancellationToken))!;
            media.SetModerationResult(request.ModerationResult);
            await _mediaRepository.UpdateAsync(media, cancellationToken);

            if (media.IsPreprocessingCompleted)
            {
                var @event = _mapper.Map<Media, MediaPreprocessingCompletedEvent>(media);
                await _publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
