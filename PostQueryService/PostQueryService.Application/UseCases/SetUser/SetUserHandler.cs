using AutoMapper;
using MediatR;
using PostQueryService.Domain;

namespace PostQueryService.Application.UseCases.SetUser
{
    internal class SetUserHandler(IPostRepository postRepository, IMapper mapper) : IRequestHandler<SetUserRequest>
    {
        public async Task Handle(SetUserRequest request, CancellationToken cancellationToken)
        {
            var posts = await postRepository.GetByUserId(request.UserId, cancellationToken);
            
            Media? profilePhoto = 
                request.ProfilePhoto != null
                    ? mapper.Map<SetUserRequest_Media, Media>(request.ProfilePhoto)
                    : null;
            
            foreach (var post in posts)
                post.SetUser(request.UserName, request.Name, profilePhoto, request.Version);

            await postRepository.UpdateAsync(posts, cancellationToken);
        }
    }
}
