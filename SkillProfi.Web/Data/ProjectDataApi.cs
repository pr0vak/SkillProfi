using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfi.Web.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace SkillProfi.Web.Data
{
    public class ProjectDataApi : DataApi, IData<Project>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectDataApi(IHttpContextAccessor httpContextAccessor)
        {
            url = baseUrl + "Projects/";
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task Add(Project model)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
            {
                await ValidationToken(_httpContextAccessor, requestMessage);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(model),
                    Encoding.UTF8, "application/json");
                await client.SendAsync(requestMessage);
            }
        }

        public async Task Delete(int? id)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url + id))
            {
                await ValidationToken(_httpContextAccessor, requestMessage);
                await client.SendAsync(requestMessage);
            }
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Project>>(json);
            }
        }

        public async Task<Project> GetById(int? id)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url + id))
            {
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Project>(json);
            }
        }

        public async Task Update(Project model)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, url))
            {
                await ValidationToken(_httpContextAccessor, requestMessage);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(model),
                    Encoding.UTF8, "application/json");
                await client.SendAsync(requestMessage);
            }
        }
    }
}
