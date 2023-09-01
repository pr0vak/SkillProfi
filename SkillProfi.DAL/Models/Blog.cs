using System.ComponentModel.DataAnnotations;

namespace SkillProfi.DAL.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? ShortDescription { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public string? ImageUrl { get; set; }


        public static Blog CreateNullBlog()
        {
            return new Blog
            {
                Id = -1,
                Title = "Null",
                ShortDescription = "Null",
                Description = "Null",
                Created = DateTime.MinValue,
                ImageUrl = "Null"
            };
        }
    }
}
