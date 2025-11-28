using AutoMapper;
using MassTransit;
using MediaService.Domain;

namespace MediaService.Application.UseCases.SetMediaTranscodedBlobName
{
    internal class SetMediaTranscodedBlobNameConsumer(IMediaRepository repository, IMapper mapper) : IConsumer<SetMediaTranscodedBlobNameRequest>
    {
        private readonly IMediaRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<SetMediaTranscodedBlobNameRequest> context)
        {
            var media = (await _repository.GetByIdAsync(context.Message.Id, context.CancellationToken))!;
            media.SetTranscodedBlobName(context.Message.BlobName);
            await _repository.UdateAsync(media, context.CancellationToken);

            var response = _mapper.Map<Media, MediaResponse>(media);
            await context.RespondAsync(response);
        }
    }
}
