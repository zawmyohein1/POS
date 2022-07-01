using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Exceptions.DataAccess
{
    public class EntityAlreadyUpdatedException:Exception
    {
        public EntityAlreadyUpdatedException() : base() { }
        public EntityAlreadyUpdatedException(string message) : base(message) { }
        public EntityAlreadyUpdatedException(string message, Exception innerException) : base(message, innerException) { }

    }
}
