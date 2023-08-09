using Newtonsoft.Json;
using System.ComponentModel;

namespace SkillProfiWeb.Models
{
    public class SiteConfig
    {
        private static SiteConfig _instance;

        // Главная страница
        public string Motto { get; set; }
        public string HeroTitle { get; set; }
        public string HeroBtnToSend { get; set; }
        public string HeroSecondTitle { get; set; }

        // Контакты
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string UrlTelegram { get; set; }
        public string UrlVk { get; set; }
        public string UrlYoutube { get; set; }

        private SiteConfig()
        {

        }

        public async static Task<SiteConfig> GetInstance()
        {
            return await ToLoadConfig();
        }

        private async static Task<SiteConfig> ToLoadConfig()
        {
            var client = new HttpClient();
            var url = "http://46.50.174.117:5000/api/Config";
            var json = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<SiteConfig>(json);
        }
    }
}
