using MassTransit;
using MediaService.Domain;

namespace MediaService.Application.UseCases.CreateMedia
{
    internal class CreateMediaConsumer(IMediaRepository mediaRepository) : IConsumer<CreateMediaRequest>
    {
        private readonly IMediaRepository _mediaRepository = mediaRepository;

        public async Task Consume(ConsumeContext<CreateMediaRequest> context)
        {
            var media = context.Message.Media
                .Select(
                    x =>
                        new Media(
                            context.Message.Id,
                            x.ContainerName,
                            x.BlobName,
                            x.Type
                        )
                )
                .ToList();

            foreach (var mediaItem in media)
                mediaItem.Create();
            
            await _mediaRepository.CreateRangeAsync(media, context.CancellationToken);
            
            await context.RespondAsync(new CreateMediaResponse([..media.Select(x => x.Id)]));
        }
    }
}
