using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.DeletePost
{
    internal class DeletePostConsumer_QueryService(ISender sender, IMapper mapper) : IConsumer<PostDeletedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostDeletedEvent> context) =>
            _sender.Send(
                _mapper.Map<PostDeletedEvent, UpdatePostRequest>(context.Message),
                context.CancellationToken
            );
    }
}
