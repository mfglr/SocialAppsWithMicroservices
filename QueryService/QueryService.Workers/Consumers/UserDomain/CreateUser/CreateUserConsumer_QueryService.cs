//using AutoMapper;
//using MassTransit;
//using MediatR;
//using QueryService.Application.UseCases.UserUseCases.UpdateUser;
//using Shared.Events.UserService;

//namespace QueryService.Workers.Consumers.UserDomain.CreateUser
//{
//    internal class CreateUserConsumer_QueryService(ISender sender, IMapper mapper) : IConsumer<UserCreatedEvent>
//    {
//        private readonly ISender _sender = sender;
//        private readonly IMapper _mapper = mapper;

//        public Task Consume(ConsumeContext<UserCreatedEvent> context) =>
//            _sender
//                .Send(
//                    _mapper.Map<UserCreatedEvent, UpdateUserRequest>(context.Message),
//                    context.CancellationToken
//                );
//    }
//}
