using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Api.Data;
using SkillProfi.DAL.Models;

namespace SkillProfi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly DataContext db;
        private readonly ILogger<RequestsController> logger;

        public RequestsController(DataContext db, ILogger<RequestsController> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        // GET: api/<RequestsController>
        /// <summary>
        /// Получить список заявок.
        /// </summary>
        /// <returns>Список заявок.</returns>
        [HttpGet]
        [Authorize]
        public IQueryable<Request> Get()
        {
            logger.LogInformation("SELECT * FROM Requests ORDERBY Id");

            return from request in db.Requests
                   orderby request.Id
                   select request;
        }

        // GET api/<RequestsController>/5
        /// <summary>
        /// Получить информацию о заявке по Id.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <returns>Информация о заявке.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<Request> Get(int id)
        {
            logger.LogInformation("Find request by id.");
            return await db.Requests.FindAsync(id) ?? DAL.Models.Request.CreateNullRequest();
        }

        // POST api/<RequestsController>
        /// <summary>
        /// Добавить завяку в базу данных.
        /// </summary>
        /// <param name="request">Описание заявки.</param>
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody] Request request)
        {
            await db.Requests.AddAsync(request);
            await db.SaveChangesAsync();
            logger.LogInformation("Added new request.");
        }

        // PUT api/<RequestsController>
        /// <summary>
        /// Обновить информацию о заявке.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <param name="request">Обновленная информация о заявке.</param>
        [HttpPut]
        [Authorize]
        public async Task Put([FromBody] Request request)
        {
            db.Requests.Update(request);
            await db.SaveChangesAsync();
            logger.LogInformation("Updated request.");
        }

        // DELETE api/<RequestsController>/5
        /// <summary>
        /// Удалить заявку по Id.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            db.Requests.Remove(await Get(id));
            await db.SaveChangesAsync();
            logger.LogInformation("Deleted request by id.");
        }
    }
}