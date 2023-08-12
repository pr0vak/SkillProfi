using Microsoft.AspNetCore.Mvc;
using SkillProfi.DAL.Models;
using SkillProfiWebApi.Data;

namespace SkillProfiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private DataContext _db;

        public BlogsController(DataContext db)
        {
            _db = db;
        }

        // GET: api/<BlogsController>
        [HttpGet]
        public IEnumerable<Blog> Get()
        {
            return _db.Blogs;
        }

        // GET api/<BlogsController>/5
        [HttpGet("{id}")]
        public async Task<Blog> Get(int id)
        {
            return await _db.Blogs.FindAsync(id) ?? Blog.CreateNullBlog();
        }

        // POST api/<BlogsController>
        [HttpPost]
        public async Task Post([FromBody] Blog blog)
        {
            await _db.Blogs.AddAsync(blog);
            await _db.SaveChangesAsync();
        }

        // PUT api/<BlogsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Blog blog)
        {
            _db.Blogs.Update(blog);
            await _db.SaveChangesAsync();
        }

        // DELETE api/<BlogsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _db.Blogs.Remove(await Get(id));
            await _db.SaveChangesAsync();
        }
    }
}
