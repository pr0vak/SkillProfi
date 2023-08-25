using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// Получить список блогов.
        /// </summary>
        /// <returns>Список блогов.</returns>
        [HttpGet]
        public IEnumerable<Blog> Get()
        {
            return _db.Blogs;
        }

        // GET api/<BlogsController>/5
        /// <summary>
        /// Получить информацию о блоге по Id.
        /// </summary>
        /// <param name="id">Id блога.</param>
        /// <returns>Информация о блоге.</returns>
        [HttpGet("{id}")]
        public async Task<Blog> Get(int id)
        {
            return await _db.Blogs.FindAsync(id) ?? Blog.CreateNullBlog();
        }

        // POST api/<BlogsController>
        /// <summary>
        /// Добавить блог в базу данных.
        /// </summary>
        /// <param name="blog">Описание блога.</param>
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody] Blog blog)
        {
            await _db.Blogs.AddAsync(blog);
            await _db.SaveChangesAsync();
        }

        // PUT api/<BlogsController>/5
        /// <summary>
        /// Обновить информацию о блоге.
        /// </summary>
        /// <param name="id">Id блога.</param>
        /// <param name="blog">Обновленная информация о блоге.</param>
        [HttpPut("{id}")]
        [Authorize]
        public async Task Put(int id, [FromBody] Blog blog)
        {
            _db.Blogs.Update(blog);
            await _db.SaveChangesAsync();
        }

        // DELETE api/<BlogsController>/5
        /// <summary>
        /// Удалить блога по Id.
        /// </summary>
        /// <param name="id">Id блога.</param>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task Delete(int id)
        {
            _db.Blogs.Remove(await Get(id));
            await _db.SaveChangesAsync();
        }
    }
}
