using MediatR;

namespace MediaService.Application.UseCases.DeleteMedia
{
    public record DeleteMediaRequest(Guid Id) : IRequest;
}
