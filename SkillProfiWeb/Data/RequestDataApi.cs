using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace SkillProfiWeb.Data
{
    public class RequestDataApi : DataApi, IData<Request>
    {
        public RequestDataApi()
        {
            url += "Requests";

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
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

            return JsonConvert.DeserializeObject<IEnumerable<Request>>(json)
                ?? new List<Request> { Request.CreateNullRequest() };
        }

        public async Task<Request> GetById(int? id)
        {
            string json = await client.GetStringAsync(url + $"/{id}");

            return JsonConvert.DeserializeObject<Request>(json) ?? Request.CreateNullRequest();
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
