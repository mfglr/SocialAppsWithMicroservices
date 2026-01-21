using AutoMapper;
using MassTransit;
using MediatR;
using QueryService.Application.UseCases.PostUseCases.UpsertPostProjection;
using Shared.Events.PostService;

namespace QueryService.Workers.Consumers.PostDomain
{

    internal class CreatePostMapper : Profile
    {
        public CreatePostMapper()
        {
            CreateMap<PostCreatedEvent_Content, UpdatePostProjectionRequest_Content>();
            CreateMap<PostCreatedEvent, UpdatePostProjectionRequest>();
        }
    }

    internal class CreatePost_OnPostCreated_QueryService(ISender sender, IMapper mapper) : IConsumer<PostCreatedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<PostCreatedEvent> context) =>
            _sender.Send(
                _mapper.Map<PostCreatedEvent, UpdatePostProjectionRequest>(context.Message),
                context.CancellationToken
            );
    }
}
