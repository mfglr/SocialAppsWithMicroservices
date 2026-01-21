using Microsoft.AspNetCore.Mvc;
using QueryService.Api.Exceptions;
using QueryService.Application;
using QueryService.Application.Pagination;
using QueryService.Application.QueryRepositories;
using QueryService.Application.UseCases.PostUseCases;
using QueryService.Domain.PostDomain;

namespace QueryService.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class PostsController(IPostQueryRepository postQueryRepository, IPostRepository postRepository, IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IPostQueryRepository _postQueryRepository = postQueryRepository;
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet("{id:guid}")]
        public async Task<PostResponse?> GetById(Guid id, CancellationToken cancellationToken) =>
            await _postQueryRepository.GetByIdAsync(id, cancellationToken) ??
            throw new PostNotFoundException();

        [HttpGet("{userId:guid}")]
        public Task<List<PostResponse>> GetByUserId(Guid userId, [FromQuery] DateTime cursor, [FromQuery] int recordsPerPage, [FromQuery] bool Isdescending, CancellationToken cancellationToken) =>
            _postQueryRepository.GetPostsByUserId(userId, new Page<DateTime>(cursor, recordsPerPage, Isdescending), cancellationToken);


        [HttpGet]
        public async Task Test()
        {
            var post = await _postRepository.GetByIdAsync(Guid.Parse("019bdfee-ca37-74d5-ad50-b9513d8e6d04"), CancellationToken.None);
            post.Apply(6, DateTime.UtcNow, new("test", new(0, 0, 0, 0)), []);
            await _unitOfWork.CommitAsync(CancellationToken.None);
        }
    }
}
