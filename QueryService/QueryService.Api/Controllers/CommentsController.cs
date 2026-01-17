using Microsoft.AspNetCore.Mvc;
using QueryService.Api.Exceptions;
using QueryService.Application.QueryRepositories;
using QueryService.Application.UseCases.CommentUseCases;

namespace QueryService.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CommentsController(ICommentQueryRepository commentQueryRepository) : ControllerBase
    {
        private readonly ICommentQueryRepository _commentQueryRepository = commentQueryRepository;

        [HttpGet("{id:guid}")]
        public async Task<CommentResponse> GetById(Guid id, CancellationToken cancellationToken) =>
            await _commentQueryRepository.GetByIdAsync(id, cancellationToken) ??
            throw new CommentNotFoundException();
    }
}
