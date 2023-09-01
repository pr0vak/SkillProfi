using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace SkillProfi.DAL.Models
{
    public class Service
    {
        [Key]
        [Display(Name = "ID услуги")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название услуги")]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Описание услуги")]
        public string? Description { get; set; }


        public static Service CreateNullService()
        {
            return new Service
            {
                Id = -1,
                Title = "Null",
                Description = "Null"
            };
        }
    }
}
