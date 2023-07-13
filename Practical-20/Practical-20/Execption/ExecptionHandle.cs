using Newtonsoft.Json;
using Practical_20.Execption;
using System.Net;

namespace Practical_20.Execption
{
    public class ExecptionHandle
    {
        private readonly RequestDelegate _next;

        public ExecptionHandle(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if(context.Response.StatusCode == (int)HttpStatusCode.NotFound && !context.Response.HasStarted && !context.Request.Path.StartsWithSegments("/Error"))
            {
                context.Response.Redirect("/Error/NotFound");
            }
            if (context.Response.StatusCode == (int)HttpStatusCode.Ambiguous && !context.Response.HasStarted && !context.Request.Path.StartsWithSegments("/Error"))
            {
                context.Response.Redirect("/Error/Ambiguous");
            }
            if (context.Response.StatusCode == (int)HttpStatusCode.HttpVersionNotSupported && !context.Response.HasStarted && !context.Request.Path.StartsWithSegments("/Error"))
            {
                context.Response.Redirect("/Error/HttpVersionNotSupport");
            }
            if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError && !context.Response.HasStarted && !context.Request.Path.StartsWithSegments("/Error"))
            {
                context.Response.Redirect("/Error/InternalServerError");
            }
            if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest && !context.Response.HasStarted && !context.Request.Path.StartsWithSegments("/Error"))
            {
                context.Response.Redirect("/Error/BadRequest");
            }
            if (context.Response.StatusCode == (int)HttpStatusCode.RequestUriTooLong && !context.Response.HasStarted && !context.Request.Path.StartsWithSegments("/Error"))
            {
                context.Response.Redirect("/Error/RequestURLTooLong");
            }
            if (context.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed && !context.Response.HasStarted && !context.Request.Path.StartsWithSegments("/Error"))
            {
                context.Response.Redirect("/Error/MethodNotAllowed");
            }
        }

       
    }
}
public static class ExceptionHandlerMiddleware
{
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExecptionHandle>();
    }
}