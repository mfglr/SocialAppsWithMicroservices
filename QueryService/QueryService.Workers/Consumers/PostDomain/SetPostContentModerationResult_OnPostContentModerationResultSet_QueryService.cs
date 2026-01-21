using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpsertPostProjection;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain
{

    internal class SetPostContentModerationResultMapper : Profile
    {
        public SetPostContentModerationResultMapper()
        {
            CreateMap<PostContentModerationResultSetEvent_Content, UpdatePostProjectionRequest_Content>();
            CreateMap<PostContentModerationResultSetEvent, UpdatePostProjectionRequest>();
        }
    }

    internal class SetPostContentModerationResult_OnPostContentModerationResultSet_QueryService(IMapper mapper, ISender sender) : IConsumer<PostContentModerationResultSetEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostContentModerationResultSetEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<PostContentModerationResultSetEvent, UpdatePostProjectionRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
