using Newtonsoft.Json.Linq;
using SkillProfi.TelegramBot.Models;

Initialize();

void Initialize()
{
    var json = File.ReadAllText("token.json");
    var token = JObject.Parse(json)["token"].ToString();
    BotClient client = new BotClient(token);
    client.Start();
}