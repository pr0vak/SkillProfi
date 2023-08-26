namespace SkillProfi.DAL.Auth
{
    /// <summary>
    /// Класс, описывающий модель пользователя при авторизации.
    /// </summary>
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
