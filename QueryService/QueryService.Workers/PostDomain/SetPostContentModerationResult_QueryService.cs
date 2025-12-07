using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.PostDomain
{
    internal class SetPostContentModerationResult_QueryService(IMediator mediator, IMapper mapper) : IConsumer<PostContentModerationResultSetEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostContentModerationResultSetEvent> context) =>
            _mediator
                .Send(
                    _mapper.Map<PostContentModerationResultSetEvent, UpdatePostRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
