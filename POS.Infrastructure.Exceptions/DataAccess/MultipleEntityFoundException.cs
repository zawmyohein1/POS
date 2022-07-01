using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Exceptions.DataAccess
{
    public class MultipleEntityFoundException:Exception
    {
        public MultipleEntityFoundException() : base() { }
        public MultipleEntityFoundException(string message) : base(message) { }
        public MultipleEntityFoundException(string message, Exception innerException) : base(message, innerException) { }
        
    }
}
