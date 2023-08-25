using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SkillProfiWebApi.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SkillProfi.DAL.Auth;
using SkillProfi.DAL.Services;
using SkillProfi.DAL.Models;
using System.Linq;

namespace SkillProfiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private DataContext _db;

        public AccountController(DataContext db)
        {
            _db = db;
        }

        // GET: api/<AccountController>
        /// <summary>
        /// Получить список аккаунтов.
        /// </summary>
        /// <returns>Список аккаунтов.</returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<Account> Get()
        {
            return _db.Accounts;
        }

        // GET api/<AccountController>/User
        /// <summary>
        /// Получить результат авторизации по имени и паролю пользователя.
        /// </summary>
        /// <param name="login">Имя пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Результат авторизации.</returns>
        [HttpGet("login={login}&password={password}")]
        public async Task<IResult> Get(string login, string password)
        {
            // находим пользователя 
            Account? account = _db.Accounts.FirstOrDefault(p => p.Login == login
                && p.Password == Password.Hash(password));

            // если пользователь не найден, отправляем статусный код 401
            if (account is null) return Results.Unauthorized();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, account.Login) };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // формируем ответ
            var response = new
            {
                access_token = encodedJwt,
                username = account.Login
            };

            return Results.Json(response);
        }

        // GET api/<AccountController>/5
        /// <summary>
        /// Получить информацию о пользователе по Id.
        /// </summary>
        /// <param name="id">Id пользователя.</param>
        /// <returns>Информация о пользователе.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<Account> Get(int id)
        {
            return await _db.Accounts.FindAsync(id) ?? Account.CreateNullAccount();
        }

        // POST api/<AccountController>
        /// <summary>
        /// Добавить пользователя в базу данных.
        /// </summary>
        /// <param name="account">Описание пользователя.</param>
        [HttpPost]
        public async Task Post([FromBody] Account account)
        {
            account.Password = Password.Hash(account.Password);
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
        }

        // PUT api/<AccountController>/5
        /// <summary>
        /// Обновить данные пользователя.
        /// </summary>
        /// <param name="id">Id пользователя.</param>
        /// <param name="account">Обновленная информация о пользователе.</param>
        [HttpPut("{id}")]
        [Authorize]
        public async Task Put(int id, [FromBody] Account account)
        {
            account.Password = Password.Hash(account.Password);
            _db.Accounts.Update(account);
            await _db.SaveChangesAsync();
        }

        // DELETE api/<AccountController>/5
        /// <summary>
        /// Удалить польозвателя по Id.
        /// </summary>
        /// <param name="id">Id пользователя.</param>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task Delete(int id)
        {
            _db.Accounts.Remove(await Get(id));
            await _db.SaveChangesAsync();
        }
    }
}
