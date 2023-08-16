using Newtonsoft.Json;
using SkillProfi.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfi.Desktop.Data
{
    public class RequestDataApi : DataApi
    {
        public RequestDataApi()
        {
            _url += "Requests/";
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
        }

        public IEnumerable<Request> GetAll()
        {
            var json = Task.Run(() => _client.GetStringAsync(_url)).Result;
            return JsonConvert.DeserializeObject<IEnumerable<Request>>(json);
        }

        public void Update(Request request)
        {
            var url = _url + request.Id;
            Task.Run(() => _client.PutAsync(
                url,
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
                    "application/json")
                ));
        }

        public void Create(Request request)
        {
            Task.Run(() => _client.PostAsync(
                _url,
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
                    "application/json")
                ));
        }
    }
}
