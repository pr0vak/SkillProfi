using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SkillProfi.Web.ViewModels;
using SkillProfi.Web.Data;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using SkillProfi.Web.Configuration;
using SkillProfi.Domain.Services;

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
            _client = new HttpClient();
            _url = $"{Connection.BaseUrl}/api";
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
            var url = _url + "/Account/Login";
            model.Password = PasswordService.Hash(model.Password);
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(url, content);
            // Если логин и пароль ввели корректно...
            if (result.StatusCode == HttpStatusCode.OK)
            {
                // Получаем ответ с токеном и сохраняем его
                var json = JObject.Parse(await result.Content.ReadAsStringAsync());
                var token = json["accessToken"]?.ToString();
                var refreshToken = json["refreshToken"]?.ToString();
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
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("Authorization");
            HttpContext.Response.Cookies.Delete("RefreshToken");
            var url = _url + "/Token/revoke";
            await _client.PostAsync(url, null);
            return RedirectToAction("Index", "Hero");
        }
    }
}
