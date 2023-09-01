using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfi.Web.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace SkillProfi.Web.Data
{
    public class BlogDataApi : DataApi, IData<Blog>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogDataApi(IHttpContextAccessor httpContextAccessor) 
        {
            url = baseUrl + "Blogs/";
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Add(Blog model)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["Authorization"].Split(' ')[^1];
                if (!string.IsNullOrEmpty(token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(model),
                    Encoding.UTF8, "application/json");
                await client.SendAsync(requestMessage);
            }
        }

        public async Task Delete(int? id)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url + id))
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["Authorization"].Split(' ')[^1];
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                await client.SendAsync(requestMessage);
            }
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Blog>>(json);
            }
        }

        public async Task<Blog> GetById(int? id)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url + id))
            {
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Blog>(json);
            }
        }

        public async Task Update(Blog model)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, url + model.Id))
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["Authorization"].Split(' ')[^1];
                if (!string.IsNullOrEmpty(token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(model),
                    Encoding.UTF8, "application/json");
                await client.SendAsync(requestMessage);
            }
        }
    }
}
