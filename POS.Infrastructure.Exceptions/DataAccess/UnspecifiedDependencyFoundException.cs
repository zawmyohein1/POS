using System;

namespace POS.Infrastructure.Exceptions.DataAccess
{
    public class UnspecifiedDependencyFoundException:Exception
    {
        public EntityTypes DependentEntity = EntityTypes.None;

        public UnspecifiedDependencyFoundException() : base() { }
        public UnspecifiedDependencyFoundException(string message) : base(message) { }

        public UnspecifiedDependencyFoundException(string message, Exception innerException) : base(message, innerException) { }

        public UnspecifiedDependencyFoundException(EntityTypes dependentEntity) : base(string.Format("No specific{0} to set", dependentEntity.ToString()))
        {
            DependentEntity = dependentEntity;
        }

    }
}
