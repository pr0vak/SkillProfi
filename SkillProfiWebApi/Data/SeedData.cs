using Microsoft.EntityFrameworkCore;
using SkillProfi.DAL.Auth;
using SkillProfi.DAL.Services;

namespace SkillProfiWebApi.Data
{
    public class SeedData
    {
        public static bool Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                Console.WriteLine("Проверка подключения к БД...");
                using (var context = new DataContext(
           serviceProvider.GetRequiredService<
               DbContextOptions<DataContext>>()))
                {
                    if (context.Accounts.Any())
                    {
                        Console.Clear();
                        return true;
                    }

                    if (context.Accounts.ToList()
                    .Where(acc => acc.Login.ToLower() == "admin")
                    .Count() == 0)
                    {
                        context.Accounts.Add(new Account
                        {
                            Login = "admin",
                            Password = Password.Hash("admin")
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
