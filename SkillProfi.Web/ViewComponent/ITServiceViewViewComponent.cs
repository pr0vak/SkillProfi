using Microsoft.AspNetCore.Mvc;

namespace SkillProfi.Web.Component
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
