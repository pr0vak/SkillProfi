using Microsoft.AspNetCore.Mvc;
using SkillProfi.DAL.Models;

namespace SkillProfiWeb.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "SkillProfi - Контакты";
            var config = SiteConfig.GetInstance();
            return View(config);
        }
    }
}
