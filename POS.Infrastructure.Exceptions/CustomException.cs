using POS.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Exceptions
{
    [Serializable]
    public class CustomException : Exception
    {
        private CustomExceptionEnum resultCode;
        private String resultDescription;

        public CustomException()
        { }

        public CustomException(CustomExceptionEnum message)
            : base(message.ToString())
        {
            resultCode = message;
            resultDescription = CustomExceptions.ResourceManager.GetString(message.ToString());
        }

        public CustomException(CustomExceptionEnum message, Exception innerException)
            : base(message.ToString(), innerException)
        {
            resultCode = message;
            resultDescription = CustomExceptions.ResourceManager.GetString(message.ToString());
        }

        public CustomExceptionEnum ResultCode
        {
            get { return resultCode; }
        }
        public String ResultDescription
        {
            get { return resultDescription; }
        }

       
        public static string GetMessage(CustomExceptionEnum message)          
        {
            return CustomExceptions.ResourceManager.GetString(message.ToString());
        }
    }
}
