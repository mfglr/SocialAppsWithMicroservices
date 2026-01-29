using Shared.Events;

namespace PostMedia.Application.UseCases.AddPostMediaThumbnail
{
    public record AddPostMediaThumbnailRequest(Guid Id, string BlobName, Thumbnail Thumbnail) : MediatR.IRequest;
}
