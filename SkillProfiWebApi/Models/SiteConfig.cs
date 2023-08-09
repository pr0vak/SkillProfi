using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkillProfiWebApi.Models
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
        public string Address { get; set; } = "168956, г. Москва ул. Симоновский Вал д.34";
        public string Phone { get; set; } = "+1 (234) 567-8910";
        public string Fax { get; set; } = "+1 (234) 567-8910";
        public string Email { get; set; } = "example@mail.ru";
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
                Address = "168956, г. Москва ул. Симоновский Вал д.34";
                Phone = "+1 (234) 567-8910";
                Fax = "+1 (234) 567-8910";
                Email = "example@mail.ru";
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
            config["address"] = Address;
            config["phone"] = Phone;
            config["fax"] = Fax;
            config["email"] = Email;
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
                Address = config["address"]?.ToString() ?? String.Empty;
                Phone = config["phone"]?.ToString() ?? String.Empty;
                Fax = config["fax"]?.ToString() ?? String.Empty;
                Email = config["email"]?.ToString() ?? String.Empty;
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
