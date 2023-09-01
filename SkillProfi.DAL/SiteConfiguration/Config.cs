using Newtonsoft.Json.Linq;

namespace SkillProfi.DAL.SiteConfiguration
{
    public class Config
    {
        private static Config? _instance;

        // Главная страница
        public string? Motto { get; set; }
        public string? HeroTitle { get; set; }
        public string? HeroBtnToSend { get; set; }
        public string? HeroSecondTitle { get; set; }

        // Контакты
        public string? UrlTelegram { get; set; }
        public string? UrlVk { get; set; }
        public string? UrlYoutube { get; set; }


        private Config()
        {
            if (!Load())
            {
                Motto = "\"РАСШИРЯЕМ ВОЗМОЖНОСТИ\"";
                HeroTitle = "IT-КОНСАЛТИНГ БЕЗ РЕГИСТРАЦИИ И СМС";
                HeroBtnToSend = "Оставить заявку";
                HeroSecondTitle = "Оставить заявку или задать вопрос";
                UrlTelegram = "https://telegram.org";
                UrlVk = "https://vk.com";
                UrlYoutube = "https://youtube.com";

                Save();
            }
        }

        public static Config Instance
        {
            get => _instance ??= new Config();
        }

        public void Save()
        {
            var config = new JObject();
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

                var config = JObject.Parse(json);
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
