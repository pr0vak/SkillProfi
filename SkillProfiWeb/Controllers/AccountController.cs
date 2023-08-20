using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillProfi.DAL.Auth;
using SkillProfiWeb.Data;
using System.Net;

namespace SkillProfiWeb.Controllers
{
    public class AccountController : Controller
    {
        private HttpClient _client;
        private string _url;

        public AccountController()
        {
            _client = new HttpClient();
            _url = "http://46.50.174.117:5000/api/account/";
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "SkillProfi - Авторизация";
            return View(new UserLogin());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            var url = _url + $"login={model.UserName}&password={model.Password}";
            var result = await _client.GetAsync(url);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var json = await _client.GetStringAsync(url);
                var token = JObject.Parse(json)["access_token"]?.ToString();
                DataApi.Token = token;
                return RedirectToAction("Index", "Hero");
            }
            else
            {
                ViewBag.Message = "Запрос не прошел валидацию";
                return View("Index", model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            DataApi.Token = string.Empty;
            return RedirectToAction("Index", "Hero");
        }
    }
}
