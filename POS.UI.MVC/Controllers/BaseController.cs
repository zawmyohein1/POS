using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using POS.Infrastructure.Logger;
using POS.Infrastructure.Common.WebApi;
using POS.Infrastructure.IoC;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using POS.Common;

namespace POS.UI.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected IWebApiClient _webApiClient = null;
        protected ILoggerHelper _logger = null;
        protected ServiceProvider serviceProvider = null;
        public static string token = string.Empty;


        public BaseController()
        {
            serviceProvider = DependencyContainer._service.BuildServiceProvider();
            _webApiClient = serviceProvider.GetService<IWebApiClient>();
            _logger = serviceProvider.GetService<ILoggerHelper>();

        }

        protected bool VerifyResponse(HttpResponseMessage response, out string errorMessage)
        {
            bool isSuccess = false;        

            switch (response.StatusCode)
            {
                // Equivalent to HTTP status 202. Accepted indicates that the request has been accepted for further processing.
                case System.Net.HttpStatusCode.Accepted:
                    isSuccess = true;
                    errorMessage = "Request has been accepted for further processing.";
                    break;
                // Equivalent to HTTP status 502. BadGateway indicates that an intermediate proxy server received a bad response from another proxy or the origin server.
                case System.Net.HttpStatusCode.BadGateway:
                    errorMessage = "Intermediate proxy server received a bad response from another proxy or the origin server.";
                    break;
                // Equivalent to HTTP status 400. BadRequest indicates that the request could not be understood by the server. BadRequest is sent when no other error is applicable, or if the exact error is unknown or does not have its own error code.
                case System.Net.HttpStatusCode.BadRequest:
                    errorMessage = "Request could not be understood by the server.";
                    break;
                // Equivalent to HTTP status 409. Conflict indicates that the request could not be carried out because of a conflict on the server.
                case System.Net.HttpStatusCode.Conflict:
                    errorMessage = ErrorKeys.EntityAlreadyUpdated;
                    break;
                // Equivalent to HTTP status 201. Created indicates that the request resulted in a new resource created before the response was sent.
                case System.Net.HttpStatusCode.Created:
                    isSuccess = true;
                    errorMessage = "Request resulted in a new resource created before the response was sent.";
                    break;
                // Equivalent to HTTP status 417. ExpectationFailed indicates that an expectation given in an Expect header could not be met by the server.
                case System.Net.HttpStatusCode.ExpectationFailed:
                    errorMessage = "Expectation given in an Expect header could not be met by the server.";
                    break;
                // Equivalent to HTTP status 403. Forbidden indicates that the server refuses to fulfill the request.
                case System.Net.HttpStatusCode.Forbidden:
                    errorMessage = ErrorKeys.AccessDenied;
                    break;
                // Equivalent to HTTP status 504. GatewayTimeout indicates that an intermediate proxy server timed out while waiting for a response from another proxy or the origin server.
                case System.Net.HttpStatusCode.GatewayTimeout:
                    errorMessage = "Intermediate proxy server timed out while waiting for a response from another proxy or the origin server.";
                    break;
                // Equivalent to HTTP status 505. HttpVersionNotSupported indicates that the requested HTTP version is not supported by the server.
                case System.Net.HttpStatusCode.HttpVersionNotSupported:
                    errorMessage = "Requested HTTP version is not supported by the server.";
                    break;
                // Equivalent to HTTP status 500. InternalServerError indicates that a generic error has occurred on the server.
                case System.Net.HttpStatusCode.InternalServerError:
                    errorMessage = ErrorKeys.AccessDenied;
                    break;
                // Equivalent to HTTP status 404. NotFound indicates that the requested resource does not exist on the server.
                case System.Net.HttpStatusCode.NotFound:
                    errorMessage = ErrorKeys.EntityNotFound;
                    break;
                // Equivalent to HTTP status 501. NotImplemented indicates that the server does not support the requested function.
                case System.Net.HttpStatusCode.NotImplemented:
                    errorMessage = "Server does not support the requested function.";
                    break;
                // Equivalent to HTTP status 200. OK indicates that the request succeeded and that the requested information is in the response. This is the most common status code to receive.
                case System.Net.HttpStatusCode.OK:
                    isSuccess = true;
                    errorMessage = "Succeeded and that the requested information is in the response.";
                    break;
                // Equivalent to HTTP status 408. RequestTimeout indicates that the client did not send a request within the time the server was expecting the request.
                case System.Net.HttpStatusCode.RequestTimeout:
                    errorMessage = "Request within the time the server was expecting the request.";
                    break;
                // Equivalent to HTTP status 401. Unauthorized indicates that the requested resource requires authentication. The WWW-Authenticate header contains the details of how to perform the authentication.
                case System.Net.HttpStatusCode.Unauthorized:
                    errorMessage = "Requested resource requires authentication.";
                    break;
                default:
                    isSuccess = false;
                    errorMessage = string.Empty;
                    break;
            }

            if (!isSuccess)
            {
                _logger.TraceLog(errorMessage);
            }

            return isSuccess;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.RouteData.Values["controller"].ToString();
            var action = context.RouteData.Values["action"].ToString();
            if (controller.ToLower() == AnonymousRouteData.contoller && action.ToLower() == AnonymousRouteData.action)
            {
                await next();
            }
            else
            {
                bool valid = checkSessionBarer();
                if (valid)
                    await next();
                else
                    context.Result = new BadRequestObjectResult("Invalid!");
            }
        }

        private bool checkSessionBarer()
        {
            token = HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
                return true;
            else
                return false;
        }
    }   
}
