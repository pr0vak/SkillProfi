using Microsoft.AspNetCore.Mvc;
using SkillProfiWeb.Models;

namespace SkillProfiWeb.Controllers
{
    public class ContactsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "SkillProfi - Контакты";
            var config = await SiteConfig.GetInstance();
            return View(config);
        }
    }
}
