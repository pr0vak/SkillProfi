using System.ComponentModel.DataAnnotations;

namespace SkillProfi.DAL.Models
{
    public class Request
    {
        [Key, Display(Name = "Номер заявки")]
        public int Id { get; set; }

        [Required, Display(Name = "Время заявки")]
        public DateTime Created { get; set; } = DateTime.Now;

        [Required, Display(Name = "Имя")]
        public string Name { get; set; }

        [Required, Display(Name = "Текст заявки")]
        public string Message { get; set; }

        [Display(Name = "Статус заявки")]
        public string Status { get; set; } = "Получена";

        [DataType(DataType.EmailAddress)]
        [Required, Display(Name = "Контакты")]
        public string Email { get; set; }


        public static Request CreateNullRequest()
        {
            return new Request
            {
                Id = -1,
                Created = new DateTime(0),
                Name = "Null",
                Message = "Заявка не найдена",
                Status = "Null",
                Email = "Null"
            };
        }
    }
}
