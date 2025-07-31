namespace Train_Management_App.Middlewares;

public class RequestLoggingMiddleware {
    private readonly RequestDelegate _next;
    public RequestLoggingMiddleware(RequestDelegate next) {
        this._next = next;
    }
    public async Task InvokeAsync(HttpContext context) {
        var method = context.Request.Method;
        var path = context.Request.Path;
        var agent = context.Request.Headers["Agent"].ToString();
        var ip = context.Connection.RemoteIpAddress?.ToString();

        Console.WriteLine($"Request {method} {path} | Agent: {agent} | IP: {ip} |");

        await _next(context);
    }
}
