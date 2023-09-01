using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SkillProfi.Domain.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using SkillProfi.Web.Configuration;

namespace SkillProfi.Web.Services
{
    public static class TokenService
    {
        public static bool IsTokenExpired(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Получите дату и время истечения срока действия токена
            var expires = jwtToken.ValidTo;

            return expires < DateTime.UtcNow.AddHours(-6);
        }

        public static async Task<JObject> RefreshToken(string expiredToken, string refreshToken)
        {
            var httpClient = new HttpClient();

            // Установите базовый URL вашего API сервера
            httpClient.BaseAddress = new Uri(Connection.BaseUrl ?? "http://localhost:5000");

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

            return null;
        }
    }
}
