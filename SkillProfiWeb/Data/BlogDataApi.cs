using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Policy;
using System.Text;

namespace SkillProfiWeb.Data
{
    public class BlogDataApi : DataApi, IData<Blog>
    {
        public BlogDataApi() 
        {
            url = baseUrl + "Blogs/";
        }

        public async Task Add(Blog model)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
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
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                await client.SendAsync(requestMessage);
            }
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Blog>>(json);
            }
        }

        public async Task<Blog> GetById(int? id)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url + id))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Blog>(json);
            }
        }

        public async Task Update(Blog model)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, url + model.Id))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                }
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(model),
                    Encoding.UTF8, "application/json");
                await client.SendAsync(requestMessage);
            }
        }
    }
}
