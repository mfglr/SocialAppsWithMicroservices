using Microsoft.AspNetCore.Http;

namespace UserService.Application.UseCases.CreateMedia
{
    public record CreateMediaRequest(IFormFile Media) : MediatR.IRequest;
}
