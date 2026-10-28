namespace disable_forms_submission
{
    internal class DisableFormsSubmissionMiddleware
    {
        private readonly RequestDelegate next;

        public DisableFormsSubmissionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value?.StartsWith("/forms/submit", StringComparison.OrdinalIgnoreCase) == true)
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            }

            return this.next(httpContext);
        }
    }
}
