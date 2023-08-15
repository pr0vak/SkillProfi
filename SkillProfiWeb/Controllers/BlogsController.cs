using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;
using SkillProfiWeb.ViewModels;

namespace SkillProfiWeb.Controllers
{
    public class BlogsController : Controller
    {
        private IData<Blog> _data;
        private IWebHostEnvironment _appEnvironment;

        public BlogsController(IData<Blog> data, IWebHostEnvironment appEnvironment)
        {
            _data = data;
            _appEnvironment = appEnvironment;
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

        [Authorize]
        [HttpGet("Blogs/Create")]
        public IActionResult Create()
        {
            ViewData["Title"] = $"SKillProfi - Блог - Добавление";

            var blogViewModel = new BlogViewModel();

            return View(blogViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BlogViewModel blogViewModel)
        {
            var uploadedFile = blogViewModel.File;
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

            await _data.Add(new Blog
            {
                Title = blogViewModel.Title,
                Description = blogViewModel.Description,
                ShortDescription = blogViewModel.ShortDescription,
                Created = DateTime.Now,
                ImageUrl = $".{pathFile}"
            });

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet("Blogs/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Title"] = $"SKillProfi - Блог - Изменение";

            var blogViewModel = new BlogViewModel(await _data.GetById(id));

            return View(blogViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(BlogViewModel blogViewModel)
        {
            var uploadedFile = blogViewModel.File;
            string? pathFile;
            if (uploadedFile != null)
            {
                // путь к папке img
                pathFile = "/img/" + Guid.NewGuid() + "." + uploadedFile.FileName.Split('.')[^1];
                // сохраняем файл в папку img в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathFile, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                    if (!blogViewModel.ImageUrl.Contains("/default.jpg"))
                    {
                        var path = _appEnvironment.WebRootPath + blogViewModel.ImageUrl;
                        System.IO.File.Delete(path);
                    }
                }
            }
            else
            {
                pathFile = blogViewModel.ImageUrl.Remove(0, 1);
            }

            await _data.Update(new Blog
            {
                Id = blogViewModel.Id,
                Title = blogViewModel.Title,
                Created = DateTime.Now,
                Description = blogViewModel.Description,
                ShortDescription = blogViewModel.ShortDescription,
                ImageUrl = $".{pathFile}"
            });

            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpGet("Blogs/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Title"] = $"SKillProfi - Блог - Удаление";

            return View(await _data.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(Blog blog)
        {
            await _data.Delete(blog.Id);

            if (!blog.ImageUrl.Contains("/default.jpg"))
            {
                var path = _appEnvironment.WebRootPath + blog.ImageUrl;
                System.IO.File.Delete(path);
            }

            return RedirectToAction("Index");
        }
    }
}
