using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpsertPostProjection;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain
{
    internal class SetPostMediaMapper : Profile
    {
        public SetPostMediaMapper()
        {
            CreateMap<PostMediaSetEvent_Content, UpdatePostProjectionRequest_Content>();
            CreateMap<PostMediaSetEvent, UpdatePostProjectionRequest>();
        }
    }

    internal class SetPostMedia_OnPostMediaSet_QueryService(ISender sender, IMapper mapper) : IConsumer<PostMediaSetEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostMediaSetEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<PostMediaSetEvent, UpdatePostProjectionRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
