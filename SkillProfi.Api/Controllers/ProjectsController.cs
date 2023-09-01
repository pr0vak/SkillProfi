using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Api.Data;
using SkillProfi.DAL.Models;

namespace SkillProfi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly DataContext db;
        private readonly ILogger<ProjectsController> logger;

        public ProjectsController(DataContext db, ILogger<ProjectsController> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        // GET: api/<ProjectsController>
        /// <summary>
        /// Получить список проектов.
        /// </summary>
        /// <returns>Список проектов.</returns>
        [HttpGet]
        public IQueryable<Project> Get()
        {
            logger.LogInformation("SELECT * FROM Projects ORDERBY Id");

            return from project in db.Projects
                   orderby project.Id
                   select project;
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
            logger.LogInformation("Find project by id.");

            return await db.Projects.FindAsync(id) ?? Project.CreateNullProject();
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
            await db.Projects.AddAsync(project);
            await db.SaveChangesAsync();
            logger.LogInformation("Added new project.");
        }

        // PUT api/<ProjectsController>
        /// <summary>
        /// Обновить информацию о проекте.
        /// </summary>
        /// <param name="id">Id проекта.</param>
        /// <param name="project">Обновленная информация о проекте.</param>
        [HttpPut]
        [Authorize]
        public async Task Put([FromBody] Project project)
        {
            db.Projects.Update(project);
            await db.SaveChangesAsync();
            logger.LogInformation("Updated project.");
        }

        // DELETE api/<ProjectsController>/5
        /// <summary>
        /// Удалить проект по Id.
        /// </summary>
        /// <param name="id">Id проекта.</param>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            db.Projects.Remove(await Get(id));
            await db.SaveChangesAsync();
            logger.LogInformation("Deleted project by id.");
        }
    }
}
