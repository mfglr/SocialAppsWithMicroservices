using AutoMapper;
using MassTransit;
using MediaService.Domain;

namespace MediaService.Application.UseCases.SetMediaThumbnail
{
    internal class SetMediaThumbnailConsumer(IMediaRepository repository, IMapper mapper) : IConsumer<SetMediaThumbnailRequest>
    {
        private readonly IMediaRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<SetMediaThumbnailRequest> context)
        {
            var media = (await _repository.GetByIdAsync(context.Message.Id, context.CancellationToken))!;
            media.SetThumbnail(context.Message.Thumbnail);
            await _repository.UdateAsync(media, context.CancellationToken);

            var response = _mapper.Map<Media, SetMediaThumbnailResponse>(media);
            await context.RespondAsync(response);
        }
    }
}
