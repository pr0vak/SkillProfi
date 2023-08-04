namespace SkillProfiWeb.Data
{
    public abstract class DataApi
    {
        protected HttpClient client { get; set; } = new HttpClient();
        protected string url { get; set; } = "http://localhost:5000/api/";
    }
}
