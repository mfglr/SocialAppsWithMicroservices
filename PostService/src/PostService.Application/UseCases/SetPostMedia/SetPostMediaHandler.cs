using AutoMapper;
using MassTransit;
using MediatR;
using PostService.Domain;
using PostService.Domain.Exceptions;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.SetPostMedia
{
    internal class SetPostMediaHandler(IPostRepository postRepository, IPublishEndpoint publishEndpoint, IMapper mapper) : IRequestHandler<SetPostMediaRequest>
    {
        public async Task Handle(SetPostMediaRequest request, CancellationToken cancellationToken)
        {
            var post = 
                await postRepository.GetByIdAsync(request.Id,cancellationToken) ??
                throw new PostNotFoundException();

            var media = mapper.Map<IEnumerable<SetPostMediaRequest_Media>, IEnumerable<Media>>(request.Media);
            post.SetMedia(media);
            await postRepository.UpdateAsync(post, cancellationToken);

            if (post.IsValid())
            {
                var @event = mapper.Map<Post, PostPreproccessingCompletedEvent>(post);
                await publishEndpoint.Publish(@event, cancellationToken);
            }
        }
    }
}
