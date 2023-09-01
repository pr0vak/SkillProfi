using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.DAL.SiteConfiguration;
using SkillProfi.Web.ViewModels;

namespace SkillProfi.Web.Controllers
{
    public class ContactsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "SkillProfi - Контакты";
            var config = new SiteConfigViewModel(Config.Instance);

            return View(config);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(SiteConfigViewModel config)
        {
            Config.Instance.UrlTelegram = config.UrlTelegram;
            Config.Instance.UrlVk = config.UrlVk;
            Config.Instance.UrlYoutube = config.UrlYoutube;
            Config.Instance.Save();
            return RedirectToAction("Index");
        }
    }
}
