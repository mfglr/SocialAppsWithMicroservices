using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using QueryService.Application.UseCases.UserUseCases.UpdateUser;
using Shared.Events.UserService;

namespace QueryService.Workers.Consumers.UserDomain.CreateUser
{
    internal class CreateUserConsumer_QueryService(IMediator mediator, IMapper mapper) : IConsumer<UserCreatedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public Task Consume(ConsumeContext<UserCreatedEvent> context) =>
            _mediator
                .Send(
                    _mapper.Map<UserCreatedEvent, UpdateUserRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
