using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SkillProfi.Web.ViewModels;
using SkillProfi.Web.Data;
using System.Net;

namespace SkillProfi.Web.Controllers
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
            return View(new UserLoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            // Подготовка запроса и отправление
            var url = _url + $"login={model.UserName}&password={model.Password}";
            var result = await _client.GetAsync(url);
            // Если логин и пароль ввели корректно...
            if (result.StatusCode == HttpStatusCode.OK)
            {
                // Получаем ответ с токеном и сохраняем его
                var json = JObject.Parse(await _client.GetStringAsync(url));
                var token = json["token"]?.ToString();
                var refreshToken = json["refreshToken"].ToString();
                HttpContext.Response.Cookies.Append("Authorization", $"Bearer {token}");
                HttpContext.Response.Cookies.Append("RefreshToken", refreshToken);

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
            HttpContext.Response.Cookies.Delete("Authorization");
            HttpContext.Response.Cookies.Delete("RefreshToken");
            DataApi.Token = string.Empty;
            return RedirectToAction("Index", "Hero");
        }
    }
}
