using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace SkillProfiWeb.Data
{
    public class RequestDataApi : DataApi, IData<Request>
    {
        public RequestDataApi()
        {
            url = baseUrl + "Requests/";
        }

        public async Task Add(Request request)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request),
                    Encoding.UTF8, "application/json");
                await client.SendAsync(requestMessage);
            }
        }

        public async Task Delete(int? id)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url + id))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                await client.SendAsync(requestMessage);
            }
        }

        public async Task<IEnumerable<Request>> GetAll()
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Request>>(json);
            }
        }

        public async Task<Request> GetById(int? id)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url + id))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Request>(json);
            }
        }

        public async Task Update(Request request)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, url + request.Id))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request),
                    Encoding.UTF8, "application/json");
                await client.SendAsync(requestMessage);
            }
        }
    }
}
