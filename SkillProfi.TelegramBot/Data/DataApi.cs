using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillProfi.TelegramBot.Models;
using System.Text;

namespace SkillProfi.TelegramBot.Data
{
    /// <summary>
    /// Класс, описывающий подключение к Web API серверу.
    /// </summary>
    public class DataApi
    {
        private HttpClient _client;
        private string _url;

        public DataApi()
        {
            _client = new HttpClient();
            InitUrl();
        }

        public void InitUrl()
        {
            var path = @".\connection.json";

            if (!File.Exists(path))
            {
                JObject data = new JObject();
                data["ip_address"] = "localhost";
                data["port"] = "5000";
                File.WriteAllText(path, data.ToString());
            }

            var json = JObject.Parse(File.ReadAllText(path));
            _url = $"http://{json["ip_address"]}:{json["port"]}/api/";

            var test = _client.GetAsync(_url + "services").Result;
        }

        public async Task<Service[]> GetServices()
        {
            var url = _url + "Services";
            var json = await _client.GetStringAsync(url);
            var services = JsonConvert.DeserializeObject<Service[]>(json);

            return services;
        }

        public async Task<Project[]> GetProjects()
        {
            var url = _url + "Projects";
            var json = await _client.GetStringAsync(url);
            var projects = JsonConvert.DeserializeObject<Project[]>(json);

            return projects;
        }

        public async Task<Blog[]> GetBlogs()
        {
            var url = _url + "Blogs";
            var json = await _client.GetStringAsync(url);
            var blogs = JsonConvert.DeserializeObject<Blog[]>(json);

            return blogs;
        }

        public async Task<SocialLinks> GetSocialLinks()
        {
            var url = _url + "SiteConfig";
            var json = await _client.GetStringAsync(url);
            var config = JObject.Parse(json);
            var socialLinks = new SocialLinks()
            {
                Telegram = config["UrlTelegram"]?.ToString(),
                Vk = config["UrlVk"]?.ToString(),
                Youtube = config["UrlYoutube"]?.ToString()
            };

            return socialLinks;
        }

        public async Task SendRequest(Request request)
        {
            var url = _url + "Requests";
            await _client.PostAsync(url,
                    new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
                        "application/json"));
        }
    }
}
