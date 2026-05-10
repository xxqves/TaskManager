using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Contracts.TaskItem;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
        {
            var result = await _taskService.CreateAsync(request);
            return Ok(result);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProject(Guid projectId)
        {
            var tasks = await _taskService.GetByProjectAsync(projectId);
            return Ok(tasks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTaskRequest request)
        {
            var result = await _taskService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _taskService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
