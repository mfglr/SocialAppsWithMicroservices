using AutoMapper;
using MassTransit;
using MassTransit.Mediator;
using MediaService.Application.UseCases.CreateMedia;
using Shared.Events.Media;
using Shared.Events.PostService;

namespace MediaService.Workers
{
    internal class CreatePostMedia_MediaService(IMediator mediator, IPublishEndpoint publishEndpoint, IMapper mapper) : IConsumer<PostMediaCreatedEvent>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IMapper _mapper = mapper;

        public async Task Consume(ConsumeContext<PostMediaCreatedEvent> context)
        {
            var client = _mediator.CreateRequestClient<CreateMediaRequest>();
            var response = await client.GetResponse<CreateMediaResponse>(
                _mapper.Map<PostMediaCreatedEvent, CreateMediaRequest>(context.Message),
                context.CancellationToken
            );

            for (int i = 0; i < response.Message.Ids.Count; i++)
                await _publishEndpoint.Publish(
                    new MediaCreatedEvent(
                        response.Message.Ids[i],
                        context.Message.Id,
                        context.Message.Media[i].ContainerName,
                        context.Message.Media[i].BlobName,
                        context.Message.Media[i].Type
                    ),
                    context.CancellationToken
                );
        }
    }
}
