using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Contracts.Project;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequest request)
        {
            var result = await _projectService.CreateAsync(request);
            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> My()
        {
            return Ok(await _projectService.GetMyAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectRequest request)
        {
            var result = await _projectService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _projectService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
