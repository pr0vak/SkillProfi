using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Api.Data;
using SkillProfi.DAL.Models;

namespace SkillProfi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly DataContext db;
        private readonly ILogger<BlogsController> logger;

        public BlogsController(DataContext db, ILogger<BlogsController> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        // GET: api/<BlogsController>
        /// <summary>
        /// �������� ������ ������.
        /// </summary>
        /// <returns>������ ������.</returns>
        [HttpGet]
        public IQueryable<Blog> Get()
        {
            logger.LogInformation("SELECT * FROM Blogs ORDERBY Id");

            return from blog in db.Blogs
                   orderby blog.Id
                   select blog;
        }

        // GET api/<BlogsController>/5
        /// <summary>
        /// �������� ���������� � ����� �� Id.
        /// </summary>
        /// <param name="id">Id �����.</param>
        /// <returns>���������� � �����.</returns>
        [HttpGet("{id}")]
        public async Task<Blog> Get(int id)
        {
            logger.LogInformation("Find blog by id.");
            return await db.Blogs.FindAsync(id) ?? Blog.CreateNullBlog();
        }

        // POST api/<BlogsController>
        /// <summary>
        /// �������� ���� � ���� ������.
        /// </summary>
        /// <param name="blog">�������� �����.</param>
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody] Blog blog)
        {
            await db.Blogs.AddAsync(blog);
            await db.SaveChangesAsync();
            logger.LogInformation("Added new blog.");
        }

        // PUT api/<BlogsController>
        /// <summary>
        /// �������� ���������� � �����.
        /// </summary>
        /// <param name="id">Id �����.</param>
        /// <param name="blog">����������� ���������� � �����.</param>
        [HttpPut]
        [Authorize]
        public async Task Put([FromBody] Blog blog)
        {
            db.Blogs.Update(blog);
            await db.SaveChangesAsync();
            logger.LogInformation("Updated blog.");
        }

        // DELETE api/<BlogsController>/5
        /// <summary>
        /// ������� ���� �� Id.
        /// </summary>
        /// <param name="id">Id �����.</param>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            db.Blogs.Remove(await Get(id));
            await db.SaveChangesAsync();
            logger.LogInformation("Deleted blog by id.");
        }
    }
}