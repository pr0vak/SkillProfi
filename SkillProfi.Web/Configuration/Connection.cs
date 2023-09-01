using Newtonsoft.Json.Linq;

namespace SkillProfi.Web.Configuration
{
    public static class Connection
    {
        public static string? IpAddress { get; private set; }
        public static string? Port { get; private set; }
        public static string? BaseUrl { get; private set; }

        static Connection()
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

            IpAddress = json["ip_address"].ToString();
            Port = json["port"].ToString();
            BaseUrl = $"http://{IpAddress}:{Port}";
        }
    }
}
