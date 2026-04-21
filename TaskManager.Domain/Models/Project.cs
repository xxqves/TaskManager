namespace TaskManager.Domain.Models
{
    public class Project
    {
        public const int MAX_PROJECTNAME_LENGTH = 50;

        public Guid Id { get; }

        public string Name { get; } = string.Empty;

        public Guid OwnerId { get; }

        private Project(Guid id, string name, Guid ownerId)
        {
            Id = id;
            Name = name;
            OwnerId = ownerId;
        }

        public static Project Create(Guid id, string name, Guid ownerId)
        {
            if (name.Length > MAX_PROJECTNAME_LENGTH)
            {
                throw new ArgumentException($"Project name must not exceed {MAX_PROJECTNAME_LENGTH} characters.");
            }

            return new Project(id, name, ownerId);
        }
    }
}
