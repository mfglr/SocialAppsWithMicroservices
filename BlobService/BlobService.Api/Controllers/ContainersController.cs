using BlobService.Api.Abstracts;
using BlobService.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlobService.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ContainersController(IContainerService containerService) : ControllerBase
    {
        private readonly IContainerService _containerService = containerService;
        
        [Authorize("admin")]
        [HttpPost]
        public Task Create(CreateContainerRequest request, CancellationToken cancellationToken) =>
            _containerService.CreateAsync(request.ContainerName, cancellationToken);
    }
}
