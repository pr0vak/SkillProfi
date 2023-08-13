using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;

namespace SkillProfiWeb.Controllers
{
    public class HeroController : Controller
    {
        private IData<Request> _requestData;

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
            request.Status = "Получена";
            request.Created = DateTime.Now;
            _requestData.Add(request);
            return RedirectToAction("RequestSent");
        }

        [HttpGet]
        public IActionResult RequestSent() 
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit()
        {
            var model = new SiteConfigViewModel(SiteConfig.GetInstance());

			return View(model);
		}

		[HttpPost]
        [Authorize]
		public IActionResult Edit(SiteConfigViewModel config)
		{
            var siteConfig = SiteConfig.GetInstance();
			siteConfig.Motto = config.Motto;
			siteConfig.HeroTitle = config.HeroTitle;
            siteConfig.HeroBtnToSend = config.HeroBtnToSend;
            siteConfig.HeroSecondTitle = config.HeroSecondTitle;
            siteConfig.Save();

			return RedirectToAction("Index");
		}
	}
}
