using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.DAL.Models;
using SkillProfi.Web.Interfaces;
using SkillProfi.Web.ViewModels;

namespace SkillProfi.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private IData<Project> _data;
        private IWebHostEnvironment _appEnvironment;

        public ProjectsController(IData<Project> data, IWebHostEnvironment appEnvironment)
        {
            _data = data;
            _appEnvironment = appEnvironment;
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


        [Authorize]
        [HttpGet("Projects/Create")]
        public IActionResult Create()
        {
            ViewData["Title"] = $"SKillProfi - Проекты - Добавление";

            var projectViewModel = new ProjectViewModel();

            return View(projectViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProjectViewModel projectViewModel)
        {
            var uploadedFile = projectViewModel.File;
            string pathFile;

            if (uploadedFile != null)
            {
                // путь к папке img
                pathFile = "/img/" + Guid.NewGuid() + "." + uploadedFile.FileName.Split('.')[^1];
                var fullPath = _appEnvironment.WebRootPath + pathFile;
                // сохраняем файл в папку img в каталоге wwwroot
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
            }
            else
            {
                pathFile = "/img/default.jpg";
            }

            await _data.Add(new Project
            {
                Title = projectViewModel.Title,
                Description = projectViewModel.Description,
                ImageUrl = $".{pathFile}"
            });

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet("Projects/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Title"] = $"SKillProfi - Проекты - Изменение";

            var projectViewModel = new ProjectViewModel(await _data.GetById(id));

            return View(projectViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(ProjectViewModel projectViewModel)
        {
            var uploadedFile = projectViewModel.File;
            string? pathFile;
            if (uploadedFile != null)
            {
                // путь к папке img
                pathFile = "/img/" + Guid.NewGuid() + "." + uploadedFile.FileName.Split('.')[^1];
                // сохраняем файл в папку img в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathFile, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                    if (!projectViewModel.ImageUrl.Contains("/default.jpg"))
                    {
                        var path = _appEnvironment.WebRootPath + projectViewModel.ImageUrl;
                        System.IO.File.Delete(path);
                    }
                }
            }
            else
            {
                pathFile = projectViewModel.ImageUrl.Remove(0, 1);
            }

            await _data.Update(new Project
            {
                Id = projectViewModel.Id,
                Title = projectViewModel.Title,
                Description = projectViewModel.Description,
                ImageUrl = $".{pathFile}"
            });

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet("Projects/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Title"] = $"SKillProfi - Проекты - Удаление";

            return View(await _data.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(Project project)
        {
            await _data.Delete(project.Id);

            if (!project.ImageUrl.Contains("/default.jpg"))
            {
                var path = _appEnvironment.WebRootPath + project.ImageUrl;
                System.IO.File.Delete(path);
            }

            return RedirectToAction("Index");
        }
    }
}
