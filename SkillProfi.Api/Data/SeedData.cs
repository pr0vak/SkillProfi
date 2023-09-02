using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SkillProfi.DAL.Models;
using SkillProfi.Domain.Services;

namespace SkillProfi.Api.Data
{
    public class SeedData
    {
        public static bool Initialize(IServiceProvider serviceProvider, string pathToConfig)
        {
            try
            {
                Console.WriteLine("Проверка подключения к БД...");
                using var context = new DataContext(serviceProvider
                        .GetRequiredService<DbContextOptions<DataContext>>());
                var json = GetConnection(pathToConfig);

                // Проверяем, нет ли в списках аккаунтов есть аккаунт с логином, как прописан
                // в файле conneciton.json.
                var admin = (from adm in context.Accounts
                            where adm.Login.ToLower() == json["user_web"].ToString()
                            select adm).FirstOrDefault();                
                if (admin is null)
                {
                    // Если такого аккаунта нет, то создаем его.
                    context.Accounts.Add(new Account
                    {
                        Login = json["user_web"]?.ToString() ?? "admin",
                        Password = PasswordService.Hash(json["password_web"]?.ToString() ?? "admin")
                    });
                    context.SaveChanges();
                }

                Console.WriteLine("Подключение установлено!");

                context.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Запуск сервера приостановлен.");
                Console.WriteLine($"{ex.Message}\n" +
                    "Проверьте файл connection.json на корректность.");
                Console.WriteLine("\n" + ex.ToString());
                return false;
            }
        }

        public static JObject GetConnection(string path)
        {
            if (!File.Exists(path))
            {
                var data = new JObject();
                data["server_address"] = "localhost";
                data["database"] = "skillprofi";
                data["user_db"] = "admin";
                data["password_db"] = "admin";
                data["user_web"] = "admin";
                data["password_web"] = "admin";
                File.WriteAllText(path, data.ToString());
            }

            return JObject.Parse(File.ReadAllText(path));
        }
    }
}
