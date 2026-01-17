using Microsoft.AspNetCore.Mvc;
using QueryService.Api.Exceptions;
using QueryService.Application.Pagination;
using QueryService.Application.QueryRepositories;
using QueryService.Application.UseCases.PostUseCases;

namespace QueryService.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class PostsController(IPostQueryRepository postQueryRepository) : ControllerBase
    {
        private readonly IPostQueryRepository _postQueryRepository = postQueryRepository;

        [HttpGet("{id:guid}")]
        public async Task<PostResponse?> GetById(Guid id, CancellationToken cancellationToken) =>
            await _postQueryRepository.GetByIdAsync(id, cancellationToken) ??
            throw new PostNotFoundException();

        [HttpGet("{userId:guid}")]
        public Task<List<PostResponse>> GetByUserId(Guid userId,[FromQuery]DateTime cursor, [FromQuery] int recordsPerPage, [FromQuery]bool Isdescending, CancellationToken cancellationToken) =>
            _postQueryRepository.GetPostsByUserId(userId, new Page<DateTime>(cursor, recordsPerPage,Isdescending), cancellationToken);
    }
}
