using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.DeletePostMedia
{
    internal class DeletePostMedia_QueryService(ISender sender, IMapper mapper) : IConsumer<PostMediaDeletedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostMediaDeletedEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<PostMediaDeletedEvent, UpdatePostRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
