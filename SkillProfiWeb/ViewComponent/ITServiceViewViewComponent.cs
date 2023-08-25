using Microsoft.AspNetCore.Mvc;

namespace SkillProfiWeb.Component
{
    /// <summary>
    /// Добавляет меню IT Service, если пользователь авторизован.
    /// </summary>
    public class ITServiceViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
