using SkillProfiWeb.Enums;

namespace SkillProfiWeb.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public StatusType Status { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
