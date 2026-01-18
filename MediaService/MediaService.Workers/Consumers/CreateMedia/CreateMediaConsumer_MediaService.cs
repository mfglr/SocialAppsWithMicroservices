using AutoMapper;
using MassTransit;
using MediaService.Application.UseCases.CreateMedia;
using MediatR;
using Shared.Events.PostService;

namespace MediaService.Workers.Consumers.CreateMedia
{
    internal class CreateMediaConsumer_MediaService(ISender sender, IMapper mapper) : IConsumer<PostCreatedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostCreatedEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<PostCreatedEvent, CreateMediaRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
