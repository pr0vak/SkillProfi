using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SkillProfi.DAL.Auth;
using SkillProfi.DAL.Services;

namespace SkillProfiWebApi.Data
{
    public class SeedData
    {
        public static bool Initialize(IServiceProvider serviceProvider, string pathToConfig)
        {
            try
            {
                Console.WriteLine("Проверка подключения к БД...");
                using (var context = new DataContext(
           serviceProvider.GetRequiredService<
               DbContextOptions<DataContext>>()))
                {
                    var json = JObject.Parse(File.ReadAllText(pathToConfig));
                    if (context.Accounts.ToList()
                    .FirstOrDefault(acc => acc.Login.ToLower() == json["user_web"].ToString()) is null)
                    {
                        context.Accounts.Add(new Account
                        {
                            Login = json["user_web"].ToString(),
                            Password = Password.Hash(json["password_web"].ToString())
                        });
                        context.SaveChanges();
                    }
                    Console.Clear();
                    return true;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Запуск сервера приостановлен.");
                Console.WriteLine($"{ex.Message}\n" +
                    "Проверьте файл connection.json на корректность.");
                return false;
            }
        }
    }
}
