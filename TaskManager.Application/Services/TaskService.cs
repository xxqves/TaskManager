using TaskManager.Application.Contracts.TaskItem;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository, ICurrentUserService currentUserService)
        {
            _repository = taskRepository;
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public Task<Guid> ChangeStatusAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken = default)
        {
            var project = await _projectRepository.GetProjectByIdAsync(request.ProjectId, cancellationToken);

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            if (project.OwnerId != _currentUserService.UserId && _currentUserService.Role != "Admin")
            {
                throw new Exception("Forbidden");
            }

            var task = TaskItem.Create(
                    
            );
        }

        public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskItem>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateAsync(UpdateTaskRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
