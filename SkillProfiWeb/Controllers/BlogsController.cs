using Microsoft.AspNetCore.Mvc;
using SkillProfiWeb.Interfaces;
using SkillProfiWeb.Models;

namespace SkillProfiWeb.Controllers
{
    public class BlogsController : Controller
    {
        private IData<Blog> _data;

        public BlogsController(IData<Blog> data)
        {
            _data = data;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "SkillProfi - Блог";

            return View(await _data.GetAll());
        }

        [HttpGet("Blogs/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var blog = await _data.GetById(id);
            ViewData["Title"] = $"SKillProfi - Блог - {blog.Title}";

            return View(blog);
        }
    }
}
