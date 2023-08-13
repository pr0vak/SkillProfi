using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;

namespace SkillProfiWeb.Controllers
{
    [Authorize]
    public class ITServiceController : Controller
    {
        private IData<Request> _requestData;
        //private IEnumerable<SelectListItem> statuses = new List<SelectListItem>()
        //    {
        //        new SelectListItem { Text = "Получена", Value = "Получена" },
        //        new SelectListItem { Text = "В работе", Value = "В работе" },
        //        new SelectListItem { Text = "Выполнена", Value = "Выполнена" },
        //        new SelectListItem { Text = "Отклонена", Value = "Отклонена" },
        //        new SelectListItem { Text = "Отменена", Value = "Отменена" }
        //    };
        private IEnumerable<string> statuses = new List<string>()
            {
                new string("Получена"),
                new string ("В работе"),
                new string ("Выполнена"),
                new string ("Отклонена"),
                new string ("Отменена")
            };

        public ITServiceController(IData<Request> requestData)
        {
            _requestData = requestData;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "IT Service";

            return View(await _requestData.GetAll());
        }

        [HttpGet("Requests/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Statuses = statuses;
            var request = await _requestData.GetById(id);
            ViewData["Title"] = $"IT Service - Заявка #{id}";

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Request request)
        {
            await _requestData.Update(request);

            return RedirectToAction("Index");
        }
    }
}
