using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.PostDomain
{
    internal class DeletePostMedia_QueryService(IMediator mediator, IMapper mapper) : IConsumer<PostMediaDeletedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostMediaDeletedEvent> context) =>
            _mediator
                .Send(
                    _mapper.Map<PostMediaDeletedEvent, UpdatePostRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
