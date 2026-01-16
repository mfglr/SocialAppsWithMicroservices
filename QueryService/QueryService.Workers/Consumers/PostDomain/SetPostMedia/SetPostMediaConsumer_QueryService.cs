using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpdatePost;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain.SetPostMedia
{
    internal class SetPostMediaConsumer_QueryService(ISender sender, IMapper mapper) : IConsumer<PostMediaSetEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostMediaSetEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<PostMediaSetEvent, UpdatePostRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
