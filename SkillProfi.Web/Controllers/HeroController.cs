using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.DAL.Models;
using SkillProfi.DAL.SiteConfiguration;
using SkillProfi.Web.Interfaces;
using SkillProfi.Web.ViewModels;

namespace SkillProfi.Web.Controllers
{
    public class HeroController : Controller
    {
        private IData<Request> _requestData;
        private static string status;

        public HeroController(IData<Request> requestData)
        {
            _requestData = requestData;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Request request)
        {
            if (ModelState.IsValid)
            {
                request.Status = "Получена";
                request.Created = DateTime.Now;
                _requestData.Add(request);
                status = "Заявка успешно отправлена!";
            }
            else
            {
                status = "Некорректно заполнены данные!";
            }

            return RedirectToAction("RequestSent");
        }

        [HttpGet]
        public IActionResult RequestSent()
        {
            ViewData["Title"] = "SkillProfi";

            return View((object)status);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit()
        {
            ViewData["Title"] = "SkillProfi - Редактирование";

            var model = new SiteConfigViewModel(Config.Instance);

			return View(model);
		}

		[HttpPost]
        [Authorize]
		public IActionResult Edit(SiteConfigViewModel config)
		{
            var siteConfig = Config.Instance;
			siteConfig.Motto = config.Motto;
			siteConfig.HeroTitle = config.HeroTitle;
            siteConfig.HeroBtnToSend = config.HeroBtnToSend;
            siteConfig.HeroSecondTitle = config.HeroSecondTitle;
            siteConfig.Save();

			return RedirectToAction("Index");
		}
	}
}
