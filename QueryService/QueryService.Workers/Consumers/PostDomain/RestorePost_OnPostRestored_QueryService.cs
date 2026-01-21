using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpsertPostProjection;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain
{
    internal class RestorePostMapper : Profile
    {
        public RestorePostMapper()
        {
            CreateMap<PostRestoredEvent_Content, UpdatePostProjectionRequest_Content>();
            CreateMap<PostRestoredEvent, UpdatePostProjectionRequest>();
        }
    }

    internal class RestorePost_OnPostRestored_QueryService(IMapper mapper, ISender sender) : IConsumer<PostRestoredEvent>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ISender _sender = sender;

        public Task Consume(ConsumeContext<PostRestoredEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<PostRestoredEvent, UpdatePostProjectionRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
