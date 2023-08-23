using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillProfi.TelegramBot.Models;
using System.Text;

namespace SkillProfi.TelegramBot.Data
{
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
        }

        public Service[] GetServices()
        {
            var url = _url + "Services";
            var json = Task.Run(() => _client.GetStringAsync(url)).Result;
            var services = JsonConvert.DeserializeObject<Service[]>(json);

            return services;
        }

        public Project[] GetProjects()
        {
            var url = _url + "Projects";
            var json = Task.Run(() => _client.GetStringAsync(url)).Result;
            var projects = JsonConvert.DeserializeObject<Project[]>(json);

            return projects;
        }

        public Blog[] GetBlogs()
        {
            var url = _url + "Blogs";
            var json = Task.Run(() => _client.GetStringAsync(url)).Result;
            var blogs = JsonConvert.DeserializeObject<Blog[]>(json);

            return blogs;
        }

        public SocialLinks GetSocialLinks()
        {
            var url = _url + "Config";
            var json = Task.Run(() => _client.GetStringAsync(url)).Result;
            var config = JObject.Parse(json);
            var socialLinks = new SocialLinks()
            {
                Telegram = config["UrlTelegram"]?.ToString(),
                Vk = config["UrlVk"]?.ToString(),
                Youtube = config["UrlYoutube"]?.ToString()
            };

            return socialLinks;
        }

        public void SendRequest(Request request)
        {
            var url = _url + "Requests";
            Task.Run(async () =>
            {
                await _client.PostAsync(url,
                    new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
                        "application/json"));
            });
        }
    }
}
