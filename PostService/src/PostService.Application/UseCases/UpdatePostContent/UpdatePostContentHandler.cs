using AutoMapper;
using MassTransit;
using MediatR;
using PostService.Domain;
using PostService.Domain.Exceptions;
using Shared.Events.PostService;

namespace PostService.Application.UseCases.UpdatePostContent
{
    internal class UpdatePostContentHandler(IPostRepository postRepository, IPublishEndpoint publishEndpoint, IMapper mapper) : IRequestHandler<UpdatePostContentRequest>
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(UpdatePostContentRequest request, CancellationToken cancellationToken)
        {
            var content = new Content(request.Content);
            var post =
                await _postRepository.GetByIdAsync(request.Id, cancellationToken) ??
                throw new PostNotFoundException();
            post.UpdateContent(content);

            var @event = _mapper.Map<Post, PostContentUpdatedEvent>(post);
            await _publishEndpoint.Publish(@event, cancellationToken);

        }
    }
}
