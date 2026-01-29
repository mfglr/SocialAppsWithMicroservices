using Microsoft.AspNetCore.Http;
using Shared.Events;

namespace UserService.Application.UseCases.CreateMedia
{
    internal class MediaTypeExtractor
    {
        public MediaType Extract(IFormFile media)
        {
            if (media.ContentType.StartsWith("image"))
                return MediaType.Image;
            else if (media.ContentType.StartsWith("video"))
                return MediaType.Video;
            throw new Exception("Invalid media type.");
        }
    }
}
