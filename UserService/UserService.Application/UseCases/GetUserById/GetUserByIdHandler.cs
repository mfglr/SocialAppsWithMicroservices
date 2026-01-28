using AutoMapper;
using MediatR;
using Orleans;
using UserService.Domain;

namespace UserService.Application.UseCases.GetUserById
{
    internal class GetUserByIdHandler(IGrainFactory grainFactory, IMapper mapper) : IRequestHandler<GetUserByIdRequest, GetUserByIdResponse>
    {
        public async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var userGrain = grainFactory.GetGrain<IUserGrain>(request.Id);
            var user = await userGrain.Get() ?? throw new UserNotFoundException();
            return mapper.Map<User, GetUserByIdResponse>(user);
        }
    }
}
