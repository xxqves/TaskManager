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

        public async Task<Guid> ChangeStatusAsync(Guid id, Domain.Enums.TaskStatus taskStatus, CancellationToken cancellationToken = default)
        {
            var task = await _repository.GetTaskByIdAsync(id, cancellationToken);

            if (task == null)
            {
                throw new Exception("Task not found");
            }

            var project = await _projectRepository.GetProjectByIdAsync(task.ProjectId, cancellationToken);

            if (project.OwnerId != _currentUserService.UserId && _currentUserService.Role != "Admin")
            {
                throw new Exception("Forbidden");
            }

            return await _repository.UpdateAsync(id, task.Title, task.Description!, taskStatus, cancellationToken);
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
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.ProjectId,
                request.AssignedUserId,
                Domain.Enums.TaskStatus.New
            );

            return await _repository.AddAsync(task, cancellationToken);
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var task = await _repository.GetTaskByIdAsync(id, cancellationToken);

            if (task == null)
            {
                throw new Exception("Task not found");
            }

            var project = await _projectRepository.GetProjectByIdAsync(task.ProjectId, cancellationToken);

            if (project.OwnerId != _currentUserService.UserId && _currentUserService.Role != "Admin")
            {
                throw new Exception("Forbidden");
            }

            return await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<List<TaskItem>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId, cancellationToken);

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            if (project.OwnerId != _currentUserService.UserId && _currentUserService.Role != "Admin")
            {
                throw new Exception("Forbidden");
            }

            return await _repository.GetByProjectAsync(projectId, cancellationToken);
        }

        public async Task<Guid> UpdateAsync(Guid id, UpdateTaskRequest request, CancellationToken cancellationToken = default)
        {
            var task = await _repository.GetTaskByIdAsync(id, cancellationToken);

            if (task == null)
            {
                throw new Exception("Task not found");
            }

            var project = await _projectRepository.GetProjectByIdAsync(task.ProjectId, cancellationToken);

            if (project.OwnerId != _currentUserService.UserId && _currentUserService.Role != "Admin")
            {
                throw new Exception("Forbidden");
            }

            return await _repository.UpdateAsync(id, request.Title, request.Description, task.Status, cancellationToken);
        }
    }
}
