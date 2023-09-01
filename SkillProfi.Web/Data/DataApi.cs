using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;
using SkillProfi.Web.Configuration;
using SkillProfi.Web.Services;
using System.Net.Http.Headers;

namespace SkillProfi.Web.Data
{
    public abstract class DataApi
    {
        protected static HttpClient client { get; set; } = new HttpClient();
        protected static string baseUrl { get; set; }
        protected string url { get; set; }

        public static bool Init()
        {
            baseUrl = Connection.BaseUrl + "/api/";

            try
            {
                var msg = client.GetAsync(baseUrl).Result;
                return true;
            }
            catch (AggregateException)
            {
                Console.WriteLine("Проверьте настройки подключения в файле \"connection.json\"");
                return false;
            }
        }

        protected async Task ValidationToken(IHttpContextAccessor httpContextAccessor, 
            HttpRequestMessage requestMessage)
        {
            var token = httpContextAccessor.HttpContext?.Request.Cookies["Authorization"]?.Split().Last() 
                ?? string.Empty;

            if (TokenService.IsTokenExpired(token))
            {
                var refreshToken = httpContextAccessor.HttpContext?.Request.Cookies["RefreshToken"];
                var json = await TokenService.RefreshToken(token, refreshToken);

                if (json != null)
                {
                    token = json["accessToken"].ToString();
                    httpContextAccessor.HttpContext?.Response.Cookies
                        .Append("Authorization", $"Bearer {token}");
                    httpContextAccessor.HttpContext?.Response.Cookies
                        .Append("RefreshToken", json["refreshToken"].ToString());

                }
            }

            if (!string.IsNullOrEmpty(token))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
