using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.RestorePost
{
    internal class RestorePostConsumer_QueryService(IMapper mapper, ISender sender) : IConsumer<PostRestoredEvent>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<PostRestoredEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<PostRestoredEvent, UpdatePostRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
