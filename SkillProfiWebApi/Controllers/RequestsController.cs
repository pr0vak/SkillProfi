using Microsoft.AspNetCore.Mvc;
using SkillProfiWebApi.Data;
using SkillProfi.DAL.Models;
using DAL = SkillProfi.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace SkillProfiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private DataContext _db;

        public RequestsController(DataContext db)
        {
            _db = db;
        }

        // GET: api/<RequestController>
        /// <summary>
        /// Получить список заявок.
        /// </summary>
        /// <returns>Список заявок.</returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<Request> Get()
        {
            return _db.Requests;
        }

        // GET api/<RequestsController>/5
        /// <summary>
        /// Получить информацию о заявке по Id.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <returns>Информация о заявке.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<Request> Get(int id)
        {
            return await _db.Requests.FindAsync(id) ?? DAL.Request.CreateNullRequest();
        }

        // POST api/<RequestsController>
        /// <summary>
        /// Добавить заявку в базу данных.
        /// </summary>
        /// <param name="request">Описание заявки.</param>
        [HttpPost]
        public async Task Post([FromBody] Request request)
        {
            await _db.Requests.AddAsync(request);
            await _db.SaveChangesAsync();
        }

        // PUT api/<RequestsController>/5
        /// <summary>
        /// Обновить информацию о заявке.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        /// <param name="request">Обновленная информация о заявке.</param>
        [HttpPut("{id}")]
        [Authorize]
        public async Task Put(int id, [FromBody] Request request)
        {
            _db.Requests.Update(request);
            await _db.SaveChangesAsync();
        }

        // DELETE api/<RequestsController>/5
        /// <summary>
        /// Удалить заявку по Id.
        /// </summary>
        /// <param name="id">Id заявки.</param>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task Delete(int id)
        {
            _db.Requests.Remove(await Get(id));
            await _db.SaveChangesAsync();
        }
    }
}
