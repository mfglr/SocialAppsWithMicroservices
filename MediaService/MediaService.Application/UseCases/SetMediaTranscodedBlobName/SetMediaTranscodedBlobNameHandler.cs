using AutoMapper;
using MassTransit;
using MediaService.Domain;
using MediatR;
using Shared.Events.MediaService;

namespace MediaService.Application.UseCases.SetMediaTranscodedBlobName
{
    internal class SetMediaTranscodedBlobNameHandler(IMediaRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint) : IRequestHandler<SetMediaTranscodedBlobNameRequest>
    {
        private readonly IMediaRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(SetMediaTranscodedBlobNameRequest request, CancellationToken cancellationToken)
        {
            var media = (await _repository.GetByIdAsync(request.Id, cancellationToken))!;
            media.SetTranscodedBlobName(request.BlobName);
            await _repository.UpdateAsync(media, cancellationToken);

            if (media.IsPreprocessingCompleted)
            {
                var @event = _mapper.Map<Media, MediaPreprocessingCompletedEvent>(media);
                await _publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
