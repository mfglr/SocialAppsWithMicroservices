using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.SetPostContentModerationResult
{
    internal class SetPostContentModerationResultConsumer_QueryService(IMapper mapper, ISender sender) : IConsumer<PostContentModerationResultSetEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostContentModerationResultSetEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<PostContentModerationResultSetEvent, UpdatePostRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
