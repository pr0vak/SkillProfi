using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfi.Web.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace SkillProfi.Web.Data
{
    public class ServiceDataApi : DataApi, IData<Service>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceDataApi(IHttpContextAccessor httpContextAccessor) 
        {
            url = baseUrl + "Services/";
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task Add(Service service)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["Authorization"].Split(' ')[^1];
                if (!string.IsNullOrEmpty(token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["Authorization"].Split(' ')[^1];
                if (!string.IsNullOrEmpty(token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                await client.SendAsync(requestMessage);
            }
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Service>>(json);
            }
        }

        public async Task<Service> GetById(int? id)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url + id))
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["Authorization"].Split(' ')[^1];
                if (!string.IsNullOrEmpty(token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["Authorization"].Split(' ')[^1];
                if (!string.IsNullOrEmpty(token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(service),
                    Encoding.UTF8, "application/json");
                await client.SendAsync(requestMessage);
            }
        }
    }
}
