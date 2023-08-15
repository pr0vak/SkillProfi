using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.DAL.Models;
using SkillProfiWeb.ViewModels;

namespace SkillProfiWeb.Controllers
{
    public class ContactsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "SkillProfi - Контакты";
            var config = new SiteConfigViewModel(SiteConfig.GetInstance());

            return View(config);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(SiteConfigViewModel config)
        {
            SiteConfig.GetInstance().UrlTelegram = config.UrlTelegram;
            SiteConfig.GetInstance().UrlVk = config.UrlVk;
            SiteConfig.GetInstance().UrlYoutube = config.UrlYoutube;
            SiteConfig.GetInstance().Save();
            return RedirectToAction("Index");
        }
    }
}
