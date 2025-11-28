using AutoMapper;
using MassTransit;
using MediaService.Domain;

namespace MediaService.Application.UseCases.SetMediaModerationResult
{
    internal class SetMediaModerationResultConsumer(IMediaRepository mediaRepository, IMapper mapper) : IConsumer<SetMediaModerationResultRequest>
    {
        private readonly IMediaRepository _mediaRepository = mediaRepository;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<SetMediaModerationResultRequest> context)
        {
            var media = (await _mediaRepository.GetByIdAsync(context.Message.Id, context.CancellationToken))!;
            media.SetModerationResult(context.Message.ModerationResult);
            await _mediaRepository.UdateAsync(media, context.CancellationToken);

            var response = _mapper.Map<Media, MediaResponse>(media);
            await context.RespondAsync(response);
        }
    }
}
