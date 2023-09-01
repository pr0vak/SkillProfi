using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Api.Data;
using SkillProfi.Domain.Auth;
using SkillProfi.Domain.Interfaces;

namespace SkillProfi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly DataContext db;
        private readonly ITokenService tokenService;

        public TokenController(DataContext userContext, ITokenService tokenService)
        {
            this.db = userContext;
            this.tokenService = tokenService;
        }


        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            var user = db.Accounts.SingleOrDefault(u => u.Login == username);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");

            var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            db.SaveChanges();

            return Ok(new AuthenticatedResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }


        [Route("revoke")]
        [HttpPost, Authorize]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var user = db.Accounts.SingleOrDefault(u => u.Login == username);

            if (user == null) 
                return BadRequest();

            user.RefreshToken = null;
            db.SaveChanges();

            return NoContent();
        }
    }
}
