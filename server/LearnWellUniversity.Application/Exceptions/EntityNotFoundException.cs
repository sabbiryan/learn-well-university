namespace LearnWellUniversity.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("The requested resource was not found.")
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string entityName, object key) : base($"Entity \"{entityName}\" ({key}) was not found.")
        {

        }
    }
}