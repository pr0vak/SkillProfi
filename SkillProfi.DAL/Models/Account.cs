using System.ComponentModel.DataAnnotations;

namespace SkillProfi.DAL.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Login { get; set; }

        [Required]
        [MaxLength(136)]
        public string? Password { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }



        public static Account CreateNullAccount()
        {
            return new Account
            {
                Id = -1,
                Login = "Null",
                Password = "Null"
            };
        }
    }
}
