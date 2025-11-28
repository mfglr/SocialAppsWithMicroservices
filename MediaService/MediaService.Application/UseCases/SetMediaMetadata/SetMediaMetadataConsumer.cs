using AutoMapper;
using MassTransit;
using MediaService.Domain;

namespace MediaService.Application.UseCases.SetMediaMetadata
{
    internal class SetMediaMetadataConsumer(IMediaRepository mediaRepository, IMapper mapper) : IConsumer<SetMediaMetadataRequest>
    {
        private readonly IMediaRepository _mediaRepository = mediaRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<SetMediaMetadataRequest> context)
        {
            var media = (await _mediaRepository.GetByIdAsync(context.Message.Id, context.CancellationToken))!;
            media.SetMetadata(context.Message.Metadata);
            await _mediaRepository.UdateAsync(media, context.CancellationToken);

            var response = _mapper.Map<Media, MediaResponse>(media);
            await context.RespondAsync(response);
        }
    }
}
