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

    BotClient client = new BotClient(token);

    client.Start();
}