using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;

namespace SkillProfiWeb.Controllers
{
    public class ServicesController : Controller
    {
        private IData<Service> _data;

        public ServicesController(IData<Service> data)
        {
            _data = data;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "SkillProfi - Услуги";

            var services = await _data.GetAll();

            return View(services);
        }

        [Authorize]
        [HttpGet("Services/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "SkillProfi - Услуги - Редактирование";

            var service = await _data.GetById(id);

            return View(service);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(Service service)
        {
            await _data.Update(service);

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet("Services/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ViewData["Title"] = "SkillProfi - Услуги - Удаление";

            var service = await _data.GetById(id);

            return View(service);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(Service service)
        {
            await _data.Delete(service.Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewData["Title"] = "SkillProfi - Услуги - Добавление";

            var service = new Service();

            return View(service);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Service service)
        {
            await _data.Add(service);

            return RedirectToAction("Index");
        }
    }
}
