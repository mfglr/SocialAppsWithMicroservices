using MediatR;
using Shared.Objects;

namespace MediaService.Application.UseCases.CreateMedia
{
    public record CreateMediaRequest(Guid Id, IEnumerable<Media> Media) : IRequest;
}
