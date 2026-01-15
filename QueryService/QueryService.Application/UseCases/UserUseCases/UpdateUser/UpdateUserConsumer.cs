using MassTransit;
using QueryService.Domain.UserDomain;

namespace QueryService.Application.UseCases.UserUseCases.UpdateUser
{
    internal class UpdateUserConsumer(IUnitOfWork unitOfWork, IUserRepository userRepository) : IConsumer<UpdateUserRequest>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task Consume(ConsumeContext<UpdateUserRequest> context)
        {
            var prev = await _userRepository.GetByIdAsync(context.Message.Id, context.CancellationToken);

            if (prev != null && context.Message.Version <= prev.Version)
                return;

            if (prev == null && context.Message.IsDeleted)
                return;

            if (prev != null && context.Message.IsDeleted)
            {
                _userRepository.Delete(prev);
                await _unitOfWork.CommitAsync(context.CancellationToken);
                return;
            }

            var media = context.Message.Media.Where(x => !x.IsDeleted);

            if (prev != null)
            {
                prev.Set(
                    context.Message.UpdatedAt,
                    context.Message.Version,
                    context.Message.IsDeleted,
                    context.Message.Name,
                    context.Message.Username,
                    context.Message.Gender,
                    media
                );
                await _unitOfWork.CommitAsync(context.CancellationToken);
                return;
            }

            var next = new User(
                context.Message.Id,
                context.Message.CreatedAt,
                context.Message.UpdatedAt,
                context.Message.Version,
                context.Message.IsDeleted,
                context.Message.Name,
                context.Message.Username,
                context.Message.Gender,
                media
            );
            await _userRepository.CreateAsync(next, context.CancellationToken);
            await _unitOfWork.CommitAsync(context.CancellationToken);
        }
    }
}
