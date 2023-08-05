using System.ComponentModel.DataAnnotations;

namespace SkillProfiWeb.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
