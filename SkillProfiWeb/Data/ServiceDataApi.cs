using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace SkillProfiWeb.Data
{
    public class ServiceDataApi : DataApi, IData<Service>
    {
        public ServiceDataApi() 
        {
            url = baseUrl + "Services/";
        }

        public async Task Add(Service service)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(service),
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

        public async Task<IEnumerable<Service>> GetAll()
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Service>>(json);
            }
        }

        public async Task<Service> GetById(int? id)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url + id))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Service>(json);
            }
        }

        public async Task Update(Service service)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, url + service.Id))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(service),
                    Encoding.UTF8, "application/json");
                await client.SendAsync(requestMessage);
            }
        }
    }
}
