using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillProfi.Domain.Auth;
using SkillProfi.Web.Data;
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

            var token = context.Request.Cookies["Authorization"];
            var key = token?.Split(' ')[1];

            if (IsTokenExpired(key))
            {
                var refreshToken = context.Request.Cookies["RefreshToken"];
                var json = await RefreshToken(key, refreshToken);

                if (json != null)
                {
                    token = json["token"].ToString();
                    context.Response.Cookies.Append("Authorization", $"Bearer {token}");
                    context.Response.Cookies.Append("RefreshToken", json["refreshToken"].ToString());
                }
            }

            headers.Add("Authorization", token);

            await _next.Invoke(context);
        }

        private bool IsTokenExpired(string token)
        {
            if (token == null)
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Получите дату и время истечения срока действия токена
            var expires = jwtToken.ValidTo;

            // Сравните дату и время истечения с текущей датой и временем
            if (expires < DateTime.UtcNow)
            {
                // Токен просрочен
                return true;
            }

            // Токен действителен
            return false;
        }

        private async Task<JObject> RefreshToken(string expiredToken, string refreshToken)
        {
            var httpClient = new HttpClient();

            // Установите базовый URL вашего API сервера
            httpClient.BaseAddress = new Uri("http://localhost:5000");

            // Добавьте заголовок с просроченным токеном
            TokenApiModel token = new TokenApiModel
            {
                AccessToken = expiredToken,
                RefreshToken = refreshToken
            };

            // Выполните запрос на обновление токена (например, через API-метод /api/token/refresh)
            var response = await httpClient.PostAsync("/api/token/refresh", 
                new StringContent(JsonConvert.SerializeObject(token), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                // Получите новый токен из ответа сервера
                var json = JObject.Parse(await response.Content.ReadAsStringAsync());
                return json;
            }

            // Обработайте ошибку (например, если обновление токена не удалось)
            // Возможно, вам нужно будет реализовать обработку ошибок по своему усмотрению.
            return null;
        }
    }
}
