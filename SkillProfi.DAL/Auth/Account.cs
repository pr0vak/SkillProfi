using System.ComponentModel.DataAnnotations;
namespace SkillProfi.DAL.Auth
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [MaxLength(136)]
        public string Password { get; set; }
    }
}
