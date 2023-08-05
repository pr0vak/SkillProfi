using Microsoft.AspNetCore.Mvc;
using SkillProfiWeb.Interfaces;
using SkillProfiWeb.Models;

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
    }
}
