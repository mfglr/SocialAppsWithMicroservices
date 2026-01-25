using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Events.UserService;
using UserQueryService.Application.UseCases.UpsertUser;

namespace UserQueryService.Worker.Consumers
{

    internal class UpdateNameMapper : Profile
    {
        public UpdateNameMapper()
        {
            CreateMap<NameUpdatedEvent, UpsertUserRequest>();
        }
    }

    internal class UpdateName_OnNameUpdated_UserQueryService(ISender sender, IMapper mapper) : IConsumer<NameUpdatedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;
        public Task Consume(ConsumeContext<NameUpdatedEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<NameUpdatedEvent, UpsertUserRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
