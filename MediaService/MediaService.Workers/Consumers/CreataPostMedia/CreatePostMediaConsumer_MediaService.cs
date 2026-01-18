using AutoMapper;
using MassTransit;
using MediaService.Application.UseCases.CreateMedia;
using MediatR;
using Shared.Events.PostService;

namespace MediaService.Workers.Consumers.CreataPostMedia
{
    internal class CreatePostMediaConsumer_MediaService(ISender sender, IMapper mapper) : IConsumer<PostMediaCreatedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostMediaCreatedEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<PostMediaCreatedEvent,CreateMediaRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
