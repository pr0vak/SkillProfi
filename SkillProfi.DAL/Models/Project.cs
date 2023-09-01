using System.ComponentModel.DataAnnotations;

namespace SkillProfi.DAL.Models
{
    public class Project
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }


        public static Project CreateNullProject()
        {
            return new Project
            {
                Id = -1,
                Title = "Null",
                Description = "Null",
                ImageUrl = "Null"
            };
        }
    }
}
