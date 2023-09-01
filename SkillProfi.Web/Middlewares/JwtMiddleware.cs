using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillProfi.Domain.Auth;
using SkillProfi.Web.Configuration;
using SkillProfi.Web.Data;
using SkillProfi.Web.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace SkillProfi.Web.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next) {  _next = next; }

        public async Task InvokeAsync(HttpContext context)
        {
            var headers = context.Request.Headers;

            var token = context.Request.Cookies.ContainsKey("Authorization") 
                ? context.Request.Cookies["Authorization"] : string.Empty;
            var key = token?.Split().Length > 1 ? token?.Split().Last().Trim() : string.Empty;

            if (TokenService.IsTokenExpired(key))
            {
                var refreshToken = context.Request.Cookies["RefreshToken"];
                var json = await TokenService.RefreshToken(key, refreshToken);

                if (json != null)
                {
                    token = json["accessToken"].ToString();
                    context.Response.Cookies.Append("Authorization", $"Bearer {token}");
                    context.Response.Cookies.Append("RefreshToken", json["refreshToken"].ToString());
                }
            }

            headers.Add("Authorization", token);

            await _next.Invoke(context);
        }
    }
}
