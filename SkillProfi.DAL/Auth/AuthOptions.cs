using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SkillProfi.DAL.Models
{
    /// <summary>
    /// Параметры для настройки JWT токена.
    /// </summary>
    public class AuthOptions
    {
        public const string ISSUER = "http://localhost:5000"; // издатель токена
        public const string AUDIENCE = "http://localhost"; // потребитель токена
        const string KEY = "mysecretkey_secretkey!22";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
