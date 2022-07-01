using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace POS.Infrastructure.Logger
{
    public interface ILoggerHelper
    {
        void LogError(Exception ex);
        void TraceLog(string message = "", [CallerFilePath] string filepath = "", [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0);
        void Log(string message = "");
    }   
}
