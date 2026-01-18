using MediatR;
using Shared.Objects;

namespace MediaService.Application.UseCases.SetMediaThumbnail
{
    public record SetMediaThumbnailRequest(Guid Id, Thumbnail Thumbnail) : IRequest;
}
