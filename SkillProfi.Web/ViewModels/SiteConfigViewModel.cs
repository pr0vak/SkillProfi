using SkillProfi.DAL.SiteConfiguration;

namespace SkillProfi.Web.ViewModels
{
    public class SiteConfigViewModel
	{
        // Главная страница
        public string Motto { get; set; }
        public string HeroTitle { get; set; }
        public string HeroBtnToSend { get; set; }
        public string HeroSecondTitle { get; set; }

        // Контакты
        public string UrlTelegram { get; set; }
        public string UrlVk { get; set; }
        public string UrlYoutube { get; set; }

        public SiteConfigViewModel() { }

        public SiteConfigViewModel(Config config)
        {
            this.Motto = config.Motto;
            this.HeroTitle = config.HeroTitle;
            this.HeroBtnToSend = config.HeroBtnToSend;
            this.HeroSecondTitle = config.HeroSecondTitle;
            this.UrlTelegram = config.UrlTelegram;
            this.UrlVk = config.UrlVk;
            this.UrlYoutube = config.UrlYoutube;
        }
    }
}
