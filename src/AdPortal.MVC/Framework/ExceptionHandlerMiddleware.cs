using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace AdPortal.MVC.Framework
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger _logger;        
        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _next = next;    
            _logger = logger.CreateLogger("ExceptionHandling");
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                HandleExceptionAsync(context,exception);
                throw;
            }
        }

        private void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorcode = "error";
            var statuscode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();
            switch(exception)
            {
                case Exception e when (exceptionType == typeof(UnauthorizedAccessException)):
                    statuscode = HttpStatusCode.Unauthorized;
                    break;
                //todo
                default:
                    statuscode = HttpStatusCode.InternalServerError;
                    break;
            }
            /*var response = new {code = errorcode, message =exception.Message};
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType ="application/json";
            context.Response.StatusCode = (int)statuscode;*/
            
            _logger.LogTrace($"!!! Error occured at {DateTime.UtcNow}: {exception.Message}. HttpStatusCode: {statuscode}.");
            
            //pattern matching
        }
    }
}