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
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return _db.Projects;
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public async Task<Project> Get(int id)
        {
            return await _db.Projects.FindAsync(id) ?? Project.CreateNullProject();
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public async Task Post([FromBody] Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
        }

        // PUT api/<ProjectsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Project project)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _db.Projects.Remove(await Get(id));
            await _db.SaveChangesAsync();
        }
    }
}
