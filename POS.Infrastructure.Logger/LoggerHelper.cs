using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace POS.Infrastructure.Logger
{
    public class LoggerHelper : ILoggerHelper
    {
        private static readonly LoggerHelper instance = new LoggerHelper();


        public LoggerHelper()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string configFilePath = path + "\\POS.Infrastructure.Logger.dll.config";
            if (!File.Exists(configFilePath))
            {
                configFilePath = path + "bin\\POS.Infrastructure.Logger.dll.config";
            }
            FileConfigurationSource configSource = new FileConfigurationSource(configFilePath);
            LogWriterFactory logWriterFactory = new LogWriterFactory(configSource);
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.SetLogWriter(logWriterFactory.Create());
        }

        public static LoggerHelper Instance
        {
            get { return instance; }
        }

        public string DumpXML(Object domObject)
        {
            var XmlData = string.Empty;

            try
            {
                if (domObject == null)
                {
                    TraceLog("Cannot Dump NULL Object!");
                    return "Cannot Dump NULL Object!";
                }

                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(domObject.GetType());

                using (var stringWriter = new System.IO.StringWriter())
                {
                    serializer.Serialize(stringWriter, domObject);
                    XmlData = stringWriter.ToString();
                }

                TraceLog(XmlData);
            }
            catch (Exception ex)
            {
                LogError(ex);
                TraceLog("Could not log the DOM Object!");
                return "Could not log the DOM Object!";
            }
            return XmlData;
        }

        private string FormatMessage(String msg)
        {
            return msg.Replace("\n", "\n\t\t");
        }

        /// <summary>
        /// <see cref="ILoggerHelper"/>
        /// </summary>
        public void LogError(Exception ex)
        {

            var logEntry = new LogEntry();
            logEntry.Categories.Add("ErrorCat");
            logEntry.Message = String.Format("[{0}][{1}][{2}] {3}", Process.GetCurrentProcess().Id, Thread.CurrentThread.ManagedThreadId, "XIP", ex.Message);
            if (ex.InnerException != null && ex.InnerException.InnerException != null)
            {
                logEntry.ExtendedProperties.Add("InnerExceptionMessage", ex.InnerException.InnerException.Message);
            }
            else if (ex.InnerException != null)
            {
                logEntry.ExtendedProperties.Add("InnerExceptionMessage", ex.InnerException.Message);
            }
            logEntry.ExtendedProperties.Add("StackTrace", ex.StackTrace);
            logEntry.Severity = TraceEventType.Error;
            writeLog(logEntry);
        }

        /// <summary>
        /// <see cref="ILoggerHelper"/>
        /// </summary>
        public void LogError(DbEntityValidationException ex)
        {
            var logEntry = new LogEntry();
            logEntry.Categories.Add("ErrorCat");
            logEntry.Message = String.Format("[{0}][{1}][{2}] {3}", Process.GetCurrentProcess().Id, Thread.CurrentThread.ManagedThreadId, "XIP", ex.Message);
            if (ex.InnerException != null && ex.InnerException.InnerException != null)
            {
                logEntry.ExtendedProperties.Add("InnerExceptionMessage", ex.InnerException.InnerException.Message);
            }
            else if (ex.InnerException != null)
            {
                logEntry.ExtendedProperties.Add("InnerExceptionMessage", ex.InnerException.Message);
            }
            logEntry.ExtendedProperties.Add("StackTrace", ex.StackTrace);
            logEntry.Severity = TraceEventType.Error;
            writeLog(logEntry);

            foreach (var eve in ex.EntityValidationErrors)
            {
                TraceLog(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                foreach (var ve in eve.ValidationErrors)
                {
                    TraceLog(String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                }
            }
        }

        /// <summary>
        /// <see cref="ILoggerHelper"/>
        /// </summary>
        public void TraceLog(string message = "", [CallerFilePath] string filePath = "",
            [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            var logEntry = new LogEntry();
            if (!string.IsNullOrEmpty(message))
            {
                //Add message in the trace log
                logEntry.ExtendedProperties.Add("", string.Format("[{0}][{1}][{2}][{3}][{4}][{5}] {6}, ", Process.GetCurrentProcess().Id, Thread.CurrentThread.ManagedThreadId, "XIP", "XSID", "XUID", "XCID", FormatMessage(message)));
            }
            logEntry.ExtendedProperties.Add("Info:", string.Format("Method: {0}, Line: {1}, Filename: {2}"
                           , methodName, lineNumber.ToString(), filePath));
            writeLog(logEntry);
        }

        public void Log(string message)
        {
            var logEntry = new LogEntry();
            logEntry.ExtendedProperties.Add("", FormatMessage(message));
            writeLog(logEntry);
        }

        /// <summary>
        /// Makes LogEntry to file
        /// </summary>
        /// <param name="logEntry">
        ///   
        /// </param>
        private void writeLog(LogEntry logEntry)
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(logEntry);
        }

      

    }
}
