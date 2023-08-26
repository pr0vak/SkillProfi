using Newtonsoft.Json.Linq;
using System;
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
                var url = _url + $"login={login}&password={password}";
                var response = await _client.GetStringAsync(url);
                var json = JObject.Parse(response);
                Token = json["access_token"]?.ToString() ?? string.Empty;

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
