using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Events.UserService;
using UserQueryService.Application.UseCases.UpsertUser;

namespace UserQueryService.Worker.Consumers
{
    internal class UpsertUser_OnUserCreated_Mapper : Profile
    {
        public UpsertUser_OnUserCreated_Mapper()
        {
            CreateMap<UserCreatedEvent, UpsertUserRequest>();
        }
    }

    internal class UpsertUser_OnUserCreated_UserQueryService(ISender sender, IMapper mapper) : IConsumer<UserCreatedEvent>
    {
        private readonly ISender _sender = sender;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<UserCreatedEvent> context) =>
            _sender
                .Send(
                    _mapper.Map<UserCreatedEvent, UpsertUserRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
