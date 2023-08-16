using System.Net.Http;

namespace SkillProfi.Desktop.Data
{
    public abstract class DataApi
    {
        protected HttpClient _client { get; set; }
        protected string _url { get; set; }

        public static string Token { get; set; }

        public DataApi()
        {
            _client = new HttpClient();
            _url = "http://46.50.174.117:5000/api/";
        }
    }
}
