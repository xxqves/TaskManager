using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Persistence.Entities;

namespace TaskManager.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(TaskItem item, CancellationToken cancellationToken = default)
        {
            var taskEntity = new TaskItemEntity
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                ProjectId = item.ProjectId,
                AssignedUserId = item.AssignedUserId,
                Status = item.Status
            };

            await _context.AddAsync(taskEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return taskEntity.Id;
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _context.Tasks
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(cancellationToken);

            return id;
        }

        public async Task<List<TaskItem>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        {
            var taskEntities = await _context.Tasks
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var tasks = taskEntities
                .Select(x => TaskItem.Create(
                    x.Id,
                    x.Title,
                    x.Description!,
                    x.ProjectId,
                    x.AssignedUserId,
                    x.Status))
                .ToList();

            return tasks;
        }

        public async Task<TaskItem> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var taskEntity = await _context.Tasks
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (taskEntity == null)
            {
                return null!;
            }

            var task = TaskItem.Create(
                id,
                taskEntity.Title,
                taskEntity.Description!,
                taskEntity.ProjectId,
                taskEntity.AssignedUserId,
                taskEntity.Status
            );

            return task;
        }

        public async Task<Guid> UpdateAsync(Guid id, string title, string description, Domain.Enums.TaskStatus taskStatus, CancellationToken cancellationToken = default)
        {
            await _context.Tasks
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(t => t.Title, t => title)
                    .SetProperty(t => t.Description, t => description), cancellationToken);

            return id;
        }
    }
}