using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Api.Data;
using SkillProfi.DAL.Models;

namespace SkillProfi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly DataContext db;
        private readonly ILogger<RequestsController> logger;

        public ServicesController(DataContext db, ILogger<RequestsController> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        // GET: api/<ServicesController>
        /// <summary>
        /// Получить список услуг.
        /// </summary>
        /// <returns>Список услуг.</returns>
        [HttpGet]
        public IEnumerable<Service> Get()
        {
            logger.LogInformation("SELECT * FROM Services ORDERBY Id");

            return from service in db.Services
                   orderby service.Id
                   select service;
        }

        // GET api/<ServicesController>/5
        /// <summary>
        /// Получить информацию о услуге по Id.
        /// </summary>
        /// <param name="id">Id услуги.</param>
        /// <returns>Информация о услуге.</returns>
        [HttpGet("{id}")]
        public async Task<Service> Get(int id)
        {
            logger.LogInformation("Find service by id.");

            return await db.Services.FindAsync(id) ?? Service.CreateNullService();
        }

        // POST api/<ServicesController>
        /// <summary>
        /// Добавить услугу в базу данных.
        /// </summary>
        /// <param name="service">Описание услуги.</param>
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody] Service service)
        {
            await db.Services.AddAsync(service);
            await db.SaveChangesAsync();
            logger.LogInformation("Added new service.");
        }

        // PUT api/<ServicesController>/5
        /// <summary>
        /// Обновить информацию о услуге.
        /// </summary>
        /// <param name="id">Id услуги.</param>
        /// <param name="service">Обновленная информация о услуге.</param>
        [HttpPut]
        [Authorize]
        public async Task Put([FromBody] Service service)
        {
            db.Services.Update(service);
            await db.SaveChangesAsync();
            logger.LogInformation("Updated service.");
        }

        // DELETE api/<ServicesController>/5
        /// <summary>
        /// Удалить сервис по Id.
        /// </summary>
        /// <param name="id">Id сервиса.</param>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            db.Services.Remove(await Get(id));
            await db.SaveChangesAsync();
            logger.LogInformation("Deleted service by id.");
        }
    }
}
