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
        /// �������� ������ ������.
        /// </summary>
        /// <returns>������ ������.</returns>
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
        /// �������� ���������� � ������ �� Id.
        /// </summary>
        /// <param name="id">Id ������.</param>
        /// <returns>���������� � ������.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<Request> Get(int id)
        {
            logger.LogInformation("Find request by id.");
            return await db.Requests.FindAsync(id) ?? DAL.Models.Request.CreateNullRequest();
        }

        // POST api/<RequestsController>
        /// <summary>
        /// �������� ������ � ���� ������.
        /// </summary>
        /// <param name="request">�������� ������.</param>
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
        /// �������� ���������� � ������.
        /// </summary>
        /// <param name="id">Id ������.</param>
        /// <param name="request">����������� ���������� � ������.</param>
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
        /// ������� ������ �� Id.
        /// </summary>
        /// <param name="id">Id ������.</param>
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