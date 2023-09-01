using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SkillProfi.Api.Data;
using SkillProfi.DAL.Models;
using SkillProfi.Domain.Auth;
using SkillProfi.Domain.Interfaces;
using SkillProfi.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SkillProfi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly DataContext db;
        private readonly ITokenService tokenService;
        private readonly ILogger<BlogsController> logger;

        public AccountController(DataContext db, ITokenService tokenService, ILogger<BlogsController> logger)
        {
            this.db = db;
            this.tokenService = tokenService;
            this.logger = logger;
        }


        // GET: api/<AccountController>
        /// <summary>
        /// Получить список аккаунтов.
        /// </summary>
        /// <returns>Список аккаунтов.</returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<Account> GetAll()
        {
            logger.LogInformation("SELET * FROM Accounts");
            return from acc in db.Accounts
                   select acc;
        }

        // GET api/<AccountController>/Login
        /// <summary>
        /// Получить результат авторизации пользователя.
        /// </summary>
        /// <returns>Результат авторизации.</returns>
        [HttpPost("Login")]
        public async Task<IResult> Login(UserLogin user)
        {
            // находим пользователя 
            Account? account = db.Accounts.FirstOrDefault(p => p.Login == user.UserName
                && p.Password == user.Password);

            // если пользователь не найден, отправляем статусный код 401
            if (account is null)
            {
                logger.LogInformation("Некорректно введены данные пользователя.");

                return Results.Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Login)
            };

            var accessToken = tokenService.GenerateAccessToken(claims);
            var refreshToken = tokenService.GenerateRefreshToken();

            account.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            account.RefreshToken = refreshToken;

            db.Accounts.Update(account);
            await db.SaveChangesAsync();

            // формируем ответ
            var response = new TokenApiModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            logger.LogInformation("Отправлен результат авторизации пользователя.");
            return Results.Json(response);
        }

        //// GET: api/<AccountController>
        ///// <summary>
        ///// Получить список аккаунтов.
        ///// </summary>
        ///// <returns>Список аккаунтов.</returns>
        //[HttpGet]
        //[Authorize]
        //public IEnumerable<Account> Get()
        //{
        //    logger.LogInformation("SELET * FROM Accounts");
        //    return from acc in db.Accounts
        //           select acc;
        //}

        //// GET api/<AccountController>/User
        ///// <summary>
        ///// Получить результат авторизации по имени и паролю пользователя.
        ///// </summary>
        ///// <param name="login">Имя пользователя.</param>
        ///// <param name="password">Пароль пользователя.</param>
        ///// <returns>Результат авторизации.</returns>
        //[HttpGet("login={login}&password={password}")]
        //public async Task<IResult> Get(string login, string password)
        //{
        //    // находим пользователя 
        //    Account? account = db.Accounts.FirstOrDefault(p => p.Login == login
        //        && p.Password == PasswordService.Hash(password));

        //    // если пользователь не найден, отправляем статусный код 401
        //    if (account is null) return Results.Unauthorized();

        //    var claims = new List<Claim> 
        //    { 
        //        new Claim(ClaimTypes.Name, account.Login)
        //    };

        //    var accessToken = tokenService.GenerateAccessToken(claims);
        //    var refreshToken = tokenService.GenerateRefreshToken();

        //    account.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        //    account.RefreshToken = refreshToken;

        //    db.Accounts.Update(account);
        //    await db.SaveChangesAsync();

        //    // формируем ответ
        //    var response = new TokenApiModel
        //    {
        //        AccessToken = accessToken,
        //        RefreshToken = refreshToken
        //    };

        //    return Results.Json(response);
        //}
    }
}
