using System.Net;

namespace TruyenAPI.Middlewares
{
    public class ExceptionHandleMiddleware
    {
        private readonly ILogger<ExceptionHandleMiddleware> logger;
        private readonly RequestDelegate request;

        public ExceptionHandleMiddleware(ILogger<ExceptionHandleMiddleware> logger, RequestDelegate request)
        {
            this.logger = logger;
            this.request = request;
        }

        public async Task InVokeAsync(HttpContext context)
        {
            try
            {
                await request(context);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                logger.LogError(ex,errorId + " : " + ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Wrong"
                };
                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
