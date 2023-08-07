using System.ComponentModel.DataAnnotations;

namespace SkillProfiWeb.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
