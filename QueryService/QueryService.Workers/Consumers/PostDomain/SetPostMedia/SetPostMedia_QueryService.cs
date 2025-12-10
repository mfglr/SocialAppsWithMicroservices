using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.SetPostMedia
{
    internal class SetPostMedia_QueryService(IMediator mediator, IMapper mapper) : IConsumer<PostMediaSetEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostMediaSetEvent> context) =>
            _mediator
                .Send(
                    _mapper.Map<PostMediaSetEvent, UpdatePostRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
