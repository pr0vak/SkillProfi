using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Data;
using SkillProfiWeb.Interfaces;

namespace SkillProfiWeb.Controllers
{
    public class ProjectsController : Controller
    {
        private IData<Project> _data;

        public ProjectsController(IData<Project> data)
        {
            _data = data;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "SkillProfi - Проекты";

            return View(await _data.GetAll());
        }

        [HttpGet("Projects/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            var project = await _data.GetById(id);
            ViewData["Title"] = $"SKillProfi - Проекты - {project.Title}";

            return View(project);
        }
    }
}
