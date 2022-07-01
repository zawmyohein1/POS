using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Exceptions.DataAccess
{
    public class AccessDeniedException:Exception
    {
        public AccessDeniedException() : base() { }

        public AccessDeniedException(string message) : base(message) { }

        public AccessDeniedException(string message, Exception innerException) : base(message, innerException) { }

    }
}
