using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SkillProfi.Domain.Auth
{
    /// <summary>
    /// Параметры для настройки JWT токена.
    /// </summary>
    public class AuthOptions
    {
        public const string ISSUER = "WebAPI";       // издатель токена
        public const string AUDIENCE = "Web MVC";          // потребитель токена
        private const string KEY = "mysecretkey_secretkey!22@mysupersecret";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
