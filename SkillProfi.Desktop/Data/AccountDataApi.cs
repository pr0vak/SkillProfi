using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillProfi.Domain.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkillProfi.Desktop.Data
{
    /// <summary>
    /// Класс, описывающий авторизацию пользователя через Web API сервис.
    /// </summary>
    internal class AccountDataApi : DataApi
    {
        public AccountDataApi() 
        {
            base.Initialize();
            _url += "Account/";
        }

        public async Task<bool> Authorization(string login, string password)
        {
            try
            {
                var response = await _client.PostAsync(_url + "Login",
                    new StringContent(JsonConvert.SerializeObject(new
                    {
                        UserName = login,
                        Password = PasswordService.Hash(password)
                    }), Encoding.UTF8, "application/json"));

                var content = await response.Content.ReadAsStringAsync();

                MessageBox.Show(content);

                var json = JObject.Parse(await response.Content.ReadAsStringAsync());
                Token = json["accessToken"]?.ToString() ?? string.Empty;

                return true;
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("Unauthorized"))
                {
                    MessageBox.Show("Неверно введен логин или пароль!", 
                        "Ошибка авторизации",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Неверно заданы параметры подключения к Web API серверу.\nИли сервер недоступен.",
                        "Ошибка подключения",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
                return false;
            }
        }
    }
}
