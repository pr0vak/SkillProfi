using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SkillProfi.Desktop.Data
{
    internal class AccountDataApi : DataApi
    {
        public AccountDataApi() 
        {
            base.Initialize();
            _url += "Account/";
        }

        public async Task<bool> Athorization(string login, string password)
        {
            try
            {
                var url = _url + $"login={login}&password={password}";
                Token = JObject.Parse(await _client.GetStringAsync(url))["access_token"]?.ToString();

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
