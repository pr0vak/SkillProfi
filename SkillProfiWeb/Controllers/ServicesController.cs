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

        public async Task<IActionResult> Index()
        {
            return View(await _data.GetAll());
        }
    }
}
