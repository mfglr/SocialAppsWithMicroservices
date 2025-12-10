using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.DeletePost
{
    internal class DeletePost_QueryService(IMediator mediator, IMapper mapper) : IConsumer<PostDeletedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostDeletedEvent> context) =>
            _mediator.Send(
                _mapper.Map<PostDeletedEvent, UpdatePostRequest>(context.Message),
                context.CancellationToken
            );
    }
}
