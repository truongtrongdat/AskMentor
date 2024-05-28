namespace AskMentor.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.Redirect("/Auth/Login"); // Unauthorized
            }

            // Token hợp lệ, chuyển giao request tới middleware tiếp theo trong pipeline
            await _next(context);
        }
    }
}
