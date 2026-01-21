using MediatR;
using QueryService.Domain.PostDomain;

namespace QueryService.Application.UseCases.PostUseCases.UpsertPostProjection
{
    internal class UpserPostProjectionHandler(IPostRepository postRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdatePostProjectionRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task Handle(UpdatePostProjectionRequest request, CancellationToken cancellationToken)
        {
            if (!request.IsValidVersion) return;

            var post = await _postRepository.GetByIdAsync(request.Id, cancellationToken);
            if (post != null && request.Version <= post.Version) return;
            if (post == null && request.IsDeleted) return;
            if (post != null && request.IsDeleted)
            {
                _postRepository.Delete(post);
                await _unitOfWork.CommitAsync(cancellationToken);
                return;
            }
            
            var content = request.Content != null
                ? new PostContent(request.Content.Value, request.Content.ModerationResult)
                : null;
            var media = request.Media.Where(x => !x.IsDeleted).ToList();
            if (post != null)
                post.Apply(request.Version, request.UpdatedAt, content, media);
            else
            {
                post = new Post(
                    request.Id,
                    request.CreatedAt,
                    request.UpdatedAt,
                    request.UserId,
                    request.Version,
                    content,
                    media
                );
                await _postRepository.CreateAsync(post, cancellationToken);
            }

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
