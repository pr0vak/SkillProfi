using Microsoft.AspNetCore.Mvc;

namespace SkillProfiWeb.Controllers
{
    public class HeroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
