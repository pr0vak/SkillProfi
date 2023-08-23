using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;

namespace SkillProfi.Desktop.Data
{
    public abstract class DataApi
    {
        protected HttpClient _client { get; set; }
        protected string _url { get; set; }
        protected string _ipAddress { get; set; }
        protected string _port { get; set; }

        public static string Token { get; set; }

        public DataApi()
        {
            Initialize();
        }

        protected void Initialize()
        {
            _client = new HttpClient();
            var path = "./connection.json";

            if (!File.Exists(path))
            {
                _ipAddress = "localhost";
                _port = "5000";
                JObject data = new JObject();
                data["ip_address"] = _ipAddress;
                data["port"] = _port;
                File.WriteAllText(path, data.ToString());
            }

            var json = JObject.Parse(File.ReadAllText(path));
            _ipAddress = json["ip_address"].ToString();
            _port = json["port"].ToString();
            _url = $"http://{_ipAddress}:{_port}/api/";
        }
    }
}
