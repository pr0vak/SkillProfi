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

            if (db.Accounts.ToList()
                .Where(acc => acc.Login.ToLower() == "admin")
                .Count() == 0)
            {
                _db.Accounts.Add(new Account 
                { 
                    Login = "admin", 
                    Password = Password.Hash("admin") 
                });
                _db.SaveChanges();
            }
        }

        // GET: api/<AccountController>
        [HttpGet]
        [Authorize]
        public IEnumerable<Account> Get()
        {
            return _db.Accounts;
        }

        // GET api/<AccountController>/User
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
        [HttpGet("{id}")]
        [Authorize]
        public async Task<Account> Get(int id)
        {
            return await _db.Accounts.FindAsync(id);
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task Post([FromBody] Account account)
        {
            account.Password = Password.Hash(account.Password);
            await _db.Accounts.AddAsync(account);
            await _db.SaveChangesAsync();
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task Put(int id, [FromBody] Account account)
        {
            account.Password = Password.Hash(account.Password);
            _db.Accounts.Update(account);
            await _db.SaveChangesAsync();
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task Delete(int id)
        {
            _db.Accounts.Remove(await Get(id));
            await _db.SaveChangesAsync();
        }
    }
}
