using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfi.Web.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace SkillProfi.Web.Data
{
    public class RequestDataApi : DataApi, IData<Request>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestDataApi(IHttpContextAccessor httpContextAccessor)
        {
            url = baseUrl + "Requests/";
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task Add(Request request)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request),
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

        public async Task<IEnumerable<Request>> GetAll()
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                await ValidationToken(_httpContextAccessor, requestMessage);
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Request>>(json);
            }
        }

        public async Task<Request> GetById(int? id)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url + id))
            {
                await ValidationToken(_httpContextAccessor, requestMessage);
                var response = await client.SendAsync(requestMessage);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Request>(json);
            }
        }

        public async Task Update(Request request)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, url))
            {
                await ValidationToken(_httpContextAccessor, requestMessage);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request),
                    Encoding.UTF8, "application/json");
                await client.SendAsync(requestMessage);
            }
        }
    }
}
