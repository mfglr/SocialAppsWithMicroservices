using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpsertPostProjection;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain
{
    internal class DeletePostMapper : Profile
    {
        public DeletePostMapper()
        {
            CreateMap<PostDeletedEvent_Content, UpdatePostProjectionRequest_Content>();
            CreateMap<PostDeletedEvent, UpdatePostProjectionRequest>();
        }
    }

    internal class DeletePost_OnPostDeleted_QueryService(ISender sender, IMapper mapper) : IConsumer<PostDeletedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostDeletedEvent> context) =>
            _sender.Send(
                _mapper.Map<PostDeletedEvent, UpdatePostProjectionRequest>(context.Message),
                context.CancellationToken
            );
    }
}
