namespace TaskManager.Application.Contracts.TaskItem
{
    public record ChangeTaskStatusRequest(
        TaskStatus TaskStatus    
    );
}
