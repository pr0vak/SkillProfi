using Microsoft.IdentityModel.Tokens;
using SkillProfi.Domain.Auth;
using SkillProfi.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SkillProfi.Domain.Services
{
    public class TokenService : ITokenService
    {
        /// <summary>
        /// Логику для создания токена доступа.
        /// </summary>
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = AuthOptions.GetSymmetricSecurityKey();
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.Now.AddHours(48),
                signingCredentials: signinCredentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        /// <summary>
        /// Логика для создания токена обновления.
        /// </summary>
        public string GenerateRefreshToken()
        {
            var randomNumer = new byte[32];

            using (var rng = RandomNumberGenerator.Create()) 
            {
                rng.GetBytes(randomNumer);
                return Convert.ToBase64String(randomNumer);
            }
        }

        /// <summary>
        /// Логика для получения субъекта-пользователя из токена доступа с истекшим сроком действия.
        /// </summary>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateLifetime = false    // здесь мы говорим, что нас не волнует срок действия токена
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
