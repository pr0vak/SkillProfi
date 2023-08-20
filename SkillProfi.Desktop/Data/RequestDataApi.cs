using Newtonsoft.Json;
using SkillProfi.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SkillProfi.Desktop.Data
{
    public class RequestDataApi : DataApi
    {
        public RequestDataApi()
        {
            _url += "Requests/";
        }

        public async Task<IEnumerable<Request>> GetAll()
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, _url))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                var response = await _client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Request>>(json);
            }
        }

        public async Task Update(Request request)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, _url + request.Id))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request), 
                    Encoding.UTF8, "application/json");
                await _client.SendAsync(requestMessage);
            }
        }

        public async Task Create(Request request)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, _url))
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request),
                    Encoding.UTF8, "application/json");
                var message = await _client.SendAsync(requestMessage);
            }
        }
    }
}
