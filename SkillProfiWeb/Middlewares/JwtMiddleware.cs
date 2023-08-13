using SkillProfiWeb.Data;

namespace SkillProfiWeb.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next) {  _next = next; }

        public async Task InvokeAsync(HttpContext context)
        {
            var headers = context.Request.Headers;

            headers.Add("Authorization", $"Bearer {DataApi.Token}");

            await _next.Invoke(context);
        }


    }
}
