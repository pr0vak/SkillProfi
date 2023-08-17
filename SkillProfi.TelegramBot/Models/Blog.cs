namespace SkillProfi.TelegramBot.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public DateTime Created { get; set; }

        public string ImageUrl { get; set; }
    }
}
