using System.Security.Cryptography;
using System.Text;

namespace SkillProfi.DAL.Services
{
    public class Password
    {
        /// <summary>
        /// Хэширование пароля.
        /// </summary>
        /// <param name="password">Пароль в обычном формате.</param>
        /// <returns>Хэшированный пароль.</returns>
        public static string Hash(string password)
        {
            using (var hashAlg = MD5.Create())
            {
                byte[] hash = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder(hash.Length * 2);
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }

    }
}
