using System.ComponentModel.DataAnnotations;

namespace SkillProfiWeb.Models
{
    public class Request
    {
        [Key, Display(Name = "Номер заявки")]
        public int Id { get; set; }

        [Required, Display(Name = "Время заявки")]
        public DateTime Created { get; set; }

        [Required, Display(Name = "Имя")]
        public string Name { get; set; }

        [Required, Display(Name = "Текст заявки")]
        public string Message { get; set; }

        [Display(Name = "Статус заявки")]
        public string Status { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required, Display(Name = "Контакты")]
        public string Email { get; set; }

    }
}
