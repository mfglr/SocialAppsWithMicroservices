using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.RestorePost
{
    internal class RestorePost(IMapper mapper, IMediator mediator) : IConsumer<PostRestoredEvent>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        public Task Consume(ConsumeContext<PostRestoredEvent> context) =>
            _mediator
                .Send(
                    _mapper.Map<PostRestoredEvent, UpdatePostRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
