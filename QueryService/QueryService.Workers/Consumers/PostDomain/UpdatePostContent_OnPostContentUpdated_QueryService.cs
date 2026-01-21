using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpsertPostProjection;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain
{

    internal class UpdatePostContentMapper : Profile
    {
        public UpdatePostContentMapper()
        {
            CreateMap<PostContentUpdatedEvent_Content, UpdatePostProjectionRequest_Content>();
            CreateMap<PostContentUpdatedEvent, UpdatePostProjectionRequest>();
        }
    }

    internal class UpdatePostContent_OnPostContentUpdated_QueryService(ISender sender, IMapper mapper) : IConsumer<PostContentUpdatedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostContentUpdatedEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<PostContentUpdatedEvent, UpdatePostProjectionRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
