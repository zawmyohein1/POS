using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaSign.Common
{
    public class ErrorKeys
    {
        public const string InvalidInput = "InvalidInput";
        public const string ServerError = "ServerError";
        public const string DependecyNotFound = "DependecyNotFound";
        public const string EntityNotFound = "EntityNotFound";
        public const string MultipleEntityFound = "MultipleEntityFound";
        public const string EntityExists = "EntityExists";
        public const string AccessDenied = "AccessDenied";
        public const string ServiceNotAssigned = "ServiceNotAssigned";
        public const string EntityAlreadyUpdated = "EntityAlreadyUpdated";
        public const string PrinterNotAssigned = "PrinterNotAssigned";
        public const string DisplayNotMapped = "DisplayNotMapped";
        public const string ServiceConflictForPrintTicket = "ServiceConflictForPrintTicket";
        
    }
    public class MediaTypeFormats
    {
        public const string Json = "application/json";
        public const string XML = "application/xml";
    }
    public class EncryptionKey
    {
        public const string key = "sblw-3hn8-sqoy19";
    }
    public class AnonymousRouteData
    {
        public const string contoller = "users";
        public const string action = "login";
    }
}
