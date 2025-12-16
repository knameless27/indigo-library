using System.Net;
using System.Text.Json;


namespace indigoLibrary.API.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


                var response = new
                {
                    message = "Something unexpected happened!",
                    detail = ex.Message
                };


                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}