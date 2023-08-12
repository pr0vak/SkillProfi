using Newtonsoft.Json.Linq;

namespace SkillProfi.DAL.Models
{
    public class SiteConfig
    {
        private static SiteConfig _instance;

        // Главная страница
        public string Motto { get; set; } = "\"РАСШИРЯЕМ ВОЗМОЖНОСТИ\"";
        public string HeroTitle { get; set; } = "IT-КОНСАЛТИНГ БЕЗ РЕГИСТРАЦИИ И СМС";
        public string HeroBtnToSend { get; set; } = "Оставить заявку";
        public string HeroSecondTitle { get; set; } = "Оставить заявку или задать вопрос";

        // Контакты
        public string UrlTelegram { get; set; } = "https://telegram.com";
        public string UrlVk { get; set; } = "https://vk.com";
        public string UrlYoutube { get; set; } = "https://youtube.com";

        
        private SiteConfig()
        {
            if (!this.Load())
            {
                Motto = "\"РАСШИРЯЕМ ВОЗМОЖНОСТИ\"";
                HeroTitle = "IT-КОНСАЛТИНГ БЕЗ РЕГИСТРАЦИИ И СМС";
                HeroBtnToSend = "Оставить заявку";
                HeroSecondTitle = "Оставить заявку или задать вопрос";
                UrlTelegram = "https://telegram.com";
                UrlVk = "https://vk.com";
                UrlYoutube = "https://youtube.com";

                this.Save();
            }   
        }

        public static SiteConfig GetInstance()
        {
            return _instance ??= new SiteConfig();
        }

        public void Save()
        {
            JObject config = new JObject();
            config["motto"] = Motto;
            config["hero_title"] = HeroTitle;
            config["hero_btn_send"] = HeroBtnToSend;
            config["hero_second_title"] = HeroSecondTitle;
            config["url_telegram"] = UrlTelegram;
            config["url_vk"] = UrlVk;
            config["url_youtube"] = UrlYoutube;
            File.WriteAllText("config.json", config.ToString()); 
        }

        private bool Load()
        {
            try
            {
                var json = File.ReadAllText("config.json");

                JObject config = new JObject(json);
                Motto = config["motto"]?.ToString() ?? String.Empty;
                HeroTitle = config["hero_title"]?.ToString() ?? String.Empty;
                HeroBtnToSend = config["hero_btn_send"]?.ToString() ?? String.Empty;
                HeroSecondTitle = config["hero_second_title"]?.ToString() ?? String.Empty;
                UrlTelegram = config["url_telegram"]?.ToString() ?? String.Empty;
                UrlVk = config["url_vk"]?.ToString() ?? String.Empty;
                UrlYoutube = config["url_youtube"]?.ToString() ?? String.Empty;

                return true;
            }
            catch 
            { 
                return false; 
            }
        }
    }
}
