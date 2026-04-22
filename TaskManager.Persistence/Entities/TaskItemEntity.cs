namespace TaskManager.Persistence.Entities
{
    public class TaskItemEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; } 

        public Guid ProjectId { get; set; }

        public Guid AssignedUserId { get; set; }

        public TaskStatus Status { get; set; }
    }
}
