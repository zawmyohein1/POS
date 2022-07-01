using System;

namespace POS.Infrastructure.Exceptions.DataAccess
{
    public class DependencyNotFoundException:Exception
    {
        public EntityTypes EntityType { get; set; }
        public DependencyNotFoundException() : base() { }
        public DependencyNotFoundException(string message) : base(message) { }
        public DependencyNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public DependencyNotFoundException(EntityTypes entityType) : this(string.Format("Releated {0} does not exist.", entityType.ToString()))
        {
            EntityType = entityType;
        }
        public DependencyNotFoundException(EntityTypes entityType, string message) : this(message)
        {
            EntityType = entityType;
        }
    }
   
}
