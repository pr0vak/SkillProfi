using Newtonsoft.Json;
using SkillProfiWeb.Interfaces;
using SkillProfiWeb.Models;
using System.Text;

namespace SkillProfiWeb.Data
{
    public class RequestDataApi : DataApi, IData<Request>
    {
        public RequestDataApi()
        {
            url += "Requests";
        }

        public async Task Add(Request request)
        {
            await client.PostAsync(
                requestUri: url,
                content: new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
                    "application/json")
                );
        }

        public async Task Delete(int? id)
        {
            var delUrl = url + $"/{id}";
            await client.DeleteAsync(delUrl);
        }

        public async Task<IEnumerable<Request>> GetAll()
        {
            string json = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<IEnumerable<Request>>(json);
        }

        public async Task<Request> GetById(int? id)
        {
            string json = await client.GetStringAsync(url + $"/{id}");

            return JsonConvert.DeserializeObject<Request>(json);
        }

        public async Task Update(Request request)
        {
            var putUrl = url + $"/{request.Id}";
            await client.PutAsync(
                    requestUri: putUrl,
                    content: new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
                        "application/json")
                );
        }
    }
}
