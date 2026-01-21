using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpsertPostProjection;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain
{
    internal class DeletePostMediaMapper : Profile
    {
        public DeletePostMediaMapper()
        {
            CreateMap<PostMediaDeletedEvent_Content, UpdatePostProjectionRequest_Content>();
            CreateMap<PostMediaDeletedEvent, UpdatePostProjectionRequest>();
        }
    }

    internal class DeletePostMedia_OnPostMediaDeleted_QueryService(ISender sender, IMapper mapper) : IConsumer<PostMediaDeletedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostMediaDeletedEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<PostMediaDeletedEvent, UpdatePostProjectionRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
