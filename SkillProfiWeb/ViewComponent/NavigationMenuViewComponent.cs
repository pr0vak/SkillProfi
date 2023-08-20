using Microsoft.AspNetCore.Mvc;

namespace SkillProfiWeb.Component
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedStatus = RouteData?.Values["status"];
            ViewData["selected_status"] = RouteData?.Values["status"];
            return View(new[]
            {
                new string("Получена"),
                new string ("В работе"),
                new string ("Выполнена"),
                new string ("Отклонена"),
                new string ("Отменена")
            });
        }
    }
}
