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
            var path = "./connection.json";
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                JObject data = JObject.Parse(json);
                _ipAddress = data["ip_address"].ToString();
                _port = data["port"].ToString();
            }
            else
            {
                _ipAddress = "localhost";
                _port = "5000";
                JObject data = new JObject();
                data["ip_address"] = _ipAddress;
                data["port"] = _port;
                File.WriteAllText(path, data.ToString());
            }

            _client = new HttpClient();
            _url = $"http://{_ipAddress}:{_port}/api/";
        }
    }
}
