﻿using Microsoft.AspNetCore.Mvc;

namespace SkillProfiWeb.Component
{
    public class LogoutViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
