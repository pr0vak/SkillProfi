using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.DAL.Models;
using SkillProfiWebApi.Data;

namespace SkillProfiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private DataContext _db;

        public ProjectsController(DataContext db)
        {
            _db = db;
        }

        // GET: api/<ProjectsController>
        /// <summary>
        /// Получить список проектов.
        /// </summary>
        /// <returns>Список проектов.</returns>
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return _db.Projects;
        }

        // GET api/<ProjectsController>/5
        /// <summary>
        /// Получить информацию о проекте по Id.
        /// </summary>
        /// <param name="id">Id проекта.</param>
        /// <returns>Информация о проекте.</returns>
        [HttpGet("{id}")]
        public async Task<Project> Get(int id)
        {
            return await _db.Projects.FindAsync(id) ?? Project.CreateNullProject();
        }

        // POST api/<ProjectsController>
        /// <summary>
        /// Добавить проект в базу данных.
        /// </summary>
        /// <param name="project">Описание проекта.</param>
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody] Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
        }

        // PUT api/<ProjectsController>/5
        /// <summary>
        /// Обновить информацию о проекте.
        /// </summary>
        /// <param name="id">Id проекта.</param>
        /// <param name="project">Обновленная информация о проекте.</param>
        [HttpPut("{id}")]
        [Authorize]
        public async Task Put(int id, [FromBody] Project project)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
        }

        // DELETE api/<ProjectsController>/5
        /// <summary>
        /// Удалить проект по Id.
        /// </summary>
        /// <param name="id">Id проекта.</param>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task Delete(int id)
        {
            _db.Projects.Remove(await Get(id));
            await _db.SaveChangesAsync();
        }
    }
}
