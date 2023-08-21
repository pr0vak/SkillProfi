using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;
using SkillProfiWeb.ViewModels;
using System.Linq.Expressions;

namespace SkillProfiWeb.Controllers
{
    [Authorize]
    public class ITServiceController : Controller
    {
        private IData<Request> _requestData;
        private IEnumerable<string> statuses = new List<string>()
            {
                new string("Получена"),
                new string ("В работе"),
                new string ("Выполнена"),
                new string ("Отклонена"),
                new string ("Отменена")
            };

        public int PageSize = 10;

        public ITServiceController(IData<Request> requestData)
        {
            _requestData = requestData;
        }

        public async Task<IActionResult> Index(string status, int requestPage = 1)
        {
            ViewData["Title"] = "IT Service";

            IEnumerable<Request> requests;
            if (string.IsNullOrEmpty(status))
            {
                requests = await _requestData.GetAll();
            }
            else
            {
                requests = (await _requestData.GetAll()).Where(p => p.Status == status);
            }
            return View(new RequestsListViewModel
            {
                Requests = (requests)
                        .Skip((requestPage - 1) * PageSize)
                        .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = requestPage,
                    ItemsPerPage = PageSize,
                    TotalItems = status == null ?
                        requests.Count() :
                        requests.Where(e => e.Status == status).Count()
                },
                CurrentStatus = status
            });
        }

        [HttpGet("Requests/{id:int}")]
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
