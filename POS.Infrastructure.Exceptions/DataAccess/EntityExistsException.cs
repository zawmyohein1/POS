using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Exceptions
{
    public class EntityExistsException:Exception
    {
        public EntityExistsException():base(){ }
        public EntityExistsException(string message) : base(message) { }
        public EntityExistsException(string message, Exception innerException):base(message,innerException) { }
                
    }
}
