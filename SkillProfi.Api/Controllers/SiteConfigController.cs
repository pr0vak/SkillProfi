using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SkillProfi.Api.Data;
using SkillProfi.DAL.SiteConfiguration;

namespace SkillProfi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteConfigController
    {
        private readonly DataContext db;
        private readonly ILogger<SiteConfigController> logger;

        public SiteConfigController(DataContext db, ILogger<SiteConfigController> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        // GET: api/<SiteConfigController>
        /// <summary>
        /// Получить данные файла конфигурации сайта.
        /// </summary>
        /// <returns>Конфигурация сайта.</returns>
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(Config.Instance);
        }
    }
}
