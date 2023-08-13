using Microsoft.AspNetCore.Mvc;

namespace SkillProfiWeb.Component
{
    public class DesktopViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
