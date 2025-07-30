using System.Net;
using System.Text.Json;

namespace Train_Management_App.Middlewares {
    public class ExceptionHandlingMiddleware {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) {
            this._next = next;
            this._logger = logger;
        }
        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context); 
            } catch (Exception ex) {
                _logger.LogError(ex, "Exception has been occurred");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
            }
        }
    }
}
