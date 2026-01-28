using AutoMapper;
using MassTransit;
using MediatR;
using Shared.Events.UserService;
using UserQueryService.Application.UseCases.UpsertUser;

namespace UserQueryService.Worker.Consumers
{
    internal class UpsertUser_OnUserUpdated_Mapper : Profile
    {
        public UpsertUser_OnUserUpdated_Mapper()
        {
            CreateMap<UserUpdatedEvent, UpsertUserRequest>();
        }
    }

    internal class UpsertUser_OnUserUpdated_UserQueryService(ISender sender, IMapper mapper) : IConsumer<UserUpdatedEvent>
    {
        public Task Consume(ConsumeContext<UserUpdatedEvent> context) =>
            sender
                .Send(
                    mapper.Map<UserUpdatedEvent, UpsertUserRequest>(context.Message),
                    context.CancellationToken
                );
    }
}
