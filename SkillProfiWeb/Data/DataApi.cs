using Newtonsoft.Json.Linq;

namespace SkillProfiWeb.Data
{
    public abstract class DataApi
    {
        internal HttpClient client { get; set; } = new HttpClient();
        internal string url { get; set; }
        internal static string Token { get; set; }

        protected void Init()
        {
            var path = "./connection.json";
            if (!File.Exists(path))
            {
                var dataConnection = new JObject();
                dataConnection["ip_address"] = "localhost";
                dataConnection["port"] = "5000";
                File.WriteAllText(path, dataConnection.ToString());
            }

            var json = JObject.Parse(File.ReadAllText(path));
            var ip_address = json["ip_address"].ToString();
            var port = json["port"].ToString();
            url = $"http://{ip_address}:{port}/api/";
        }
    }
}
