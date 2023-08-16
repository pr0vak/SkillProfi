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
            _url += "Account/";
        }

        public bool Athorization(string login, string password)
        {
            try
            {
                var url = _url + $"login={login}&password={password}";
                var json = Task.Run(() => _client.GetStringAsync(url)).Result;
                Token = JObject.Parse(json)["access_token"]?.ToString();

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
                        "Неверно заданы параметры подключения к Web API серверу!\nПроверьте ссылку.",
                        "Ошибка соединения",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
                return false;
            }
        }
    }
}
