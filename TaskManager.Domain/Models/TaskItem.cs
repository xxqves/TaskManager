namespace TaskManager.Domain.Models
{
    public class TaskItem
    {
        public const int MAX_TASKITEM_TITLE_LENGTH = 50;

        public const int MAX_TASKITEM_DESCRIPTION_LENGTH = 250;

        public Guid Id { get; }

        public string Title { get; } = string.Empty;

        public string? Description { get; }

        public Guid ProjectId { get; }

        public Guid AssignedUserId { get; }

        public TaskStatus Status { get; }

        private TaskItem(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public static TaskItem Create(Guid id, string title, string description)
        {
            if (title.Length > MAX_TASKITEM_TITLE_LENGTH)
            {
                throw new ArgumentException($"Taskitem title must not exceed {MAX_TASKITEM_TITLE_LENGTH} characters.");
            }

            if (description.Length > MAX_TASKITEM_DESCRIPTION_LENGTH)
            {
                throw new ArgumentException($"Taskitem description must not exceed {MAX_TASKITEM_DESCRIPTION_LENGTH} characters.");
            }

            return new TaskItem(id, title, description);
        }
    }
}
