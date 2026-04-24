namespace TaskManager.Persistence.Entities
{
    public class ProjectEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid OwnerId { get; set; }
    }
}
