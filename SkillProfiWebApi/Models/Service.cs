using System.ComponentModel.DataAnnotations;

namespace SkillProfiWebApi.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


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
