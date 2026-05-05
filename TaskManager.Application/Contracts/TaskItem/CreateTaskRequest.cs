namespace TaskManager.Application.Contracts.TaskItem
{
    public record CreateTaskRequest(
        string Title,
        string Description,
        Guid ProjectId,
        Guid AssignedUserId
    );
}
