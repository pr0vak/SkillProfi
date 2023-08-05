using Microsoft.AspNetCore.Mvc;
using SkillProfiWeb.Interfaces;
using SkillProfiWeb.Models;

namespace SkillProfiWeb.Controllers
{
    public class ServicesController : Controller
    {
        private IData<Service> _data;

        public ServicesController(IData<Service> data)
        {
            _data = data;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _data.GetAll());
        }
    }
}
