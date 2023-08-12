using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SkillProfi.DAL.Models;

namespace SkillProfiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        // GET: api/<ConfigController>
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(SiteConfig.GetInstance());
        }
    }
}
