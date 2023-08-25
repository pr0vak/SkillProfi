using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillProfi.DAL.Auth;
using SkillProfiWeb.Data;
using System.IO;
using System.Net;
using System.Security.Policy;

namespace SkillProfiWeb.Controllers
{
    public class AccountController : Controller
    {
        private HttpClient _client;
        private string _url;

        public AccountController()
        {
            // Инициализируем переменные для подключения к серверу
            // и авторизовывания
            var path = "./connection.json";
            _client = new HttpClient();
            var json = JObject.Parse(System.IO.File.ReadAllText(path));
            var ip_address = json["ip_address"].ToString();
            var port = json["port"].ToString();
            _url = $"http://{ip_address}:{port}/api/account/";
        }

        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            ViewData["Title"] = "SkillProfi - Авторизация";
            return View(new UserLogin() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            // Подготовка запроса и отправление
            var url = _url + $"login={model.UserName}&password={model.Password}";
            var result = await _client.GetAsync(url);
            // Если логин и пароль ввели корректно...
            if (result.StatusCode == HttpStatusCode.OK)
            {
                // Получаем ответ с токеном и сохраняем его
                var json = await _client.GetStringAsync(url);
                var token = JObject.Parse(json)["access_token"]?.ToString();
                DataApi.Token = token;

                // возвращаемся на предыдущую страницу
                return Redirect(model.ReturnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError("", "Не верный логин или пароль!");
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
