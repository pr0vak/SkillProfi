using Newtonsoft.Json.Linq;

namespace SkillProfi.Web.Data
{
    public abstract class DataApi
    {
        protected static HttpClient client { get; set; } = new HttpClient();
        protected static string baseUrl { get; set; }
        protected string url { get; set; }
        internal static string Token { get; set; }

        public static bool Init()
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
            baseUrl = $"http://{ip_address}:{port}/api/";

            try
            {
                var msg = client.GetAsync(baseUrl).Result;
                return true;
            }
            catch (AggregateException)
            {
                Console.WriteLine("Проверьте настройки подключения в файле \"connection.json\"");
                return false;
            }
        }
    }
}
