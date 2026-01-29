namespace PostMedia.Application.UseCases.DeletePost
{
    public record DeletePostRequest(Guid Id) : MediatR.IRequest;
}
