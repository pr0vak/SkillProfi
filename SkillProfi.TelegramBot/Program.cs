using Newtonsoft.Json.Linq;
using SkillProfi.TelegramBot.Models;

Initialize();

void Initialize()
{
    var path = @".\token.json";

    if (!File.Exists(path))
    {
        JObject data = new JObject();
        data["token"] = "Your Telegram Bot Token";
        File.WriteAllText(path, data.ToString());
    }

    var json = File.ReadAllText(path);
    var token = JObject.Parse(json)["token"].ToString();


    try
    {
        BotClient client = new BotClient(token);
        client.Start();
    }
    catch (AggregateException ex)
    {
        Console.WriteLine("Проверьте правильность подключения к серверу WebAPI в файле \"connection.json\"!");
        Console.ReadLine();
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine("Проверьте правильность токена в файле \"token.json\"!");
        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        Console.ReadLine();
    }
}