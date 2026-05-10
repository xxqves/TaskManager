namespace TaskManager.Application.Contracts.TaskItem
{
    public record UpdateTaskRequest(
        string Title,
        string Description
    );
}
