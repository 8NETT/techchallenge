namespace FIAP.FCG.WebApi.Middleware
{
    public static class ErroMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErroMiddleware>();
        }
    }
}
