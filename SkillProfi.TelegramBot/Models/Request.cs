
namespace SkillProfi.TelegramBot.Models
{
    public class Request
    {
        public int Id { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public string Name { get; set; }

        public string Message { get; set; }

        public string Status { get; set; } = "Получена";

        public string Email { get; set; }
    }
}
