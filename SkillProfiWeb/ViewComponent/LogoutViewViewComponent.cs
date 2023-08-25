using Microsoft.AspNetCore.Mvc;

namespace SkillProfiWeb.Component
{
    /// <summary>
    /// Добавляет в меню Выход/Вход, согласно тому, авторизован ли пользователь.
    /// </summary>
    public class LogoutViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
