using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using SkillProfiWeb.Interfaces;
using SkillProfiWeb.Models;
using System.Text;

namespace SkillProfiWeb.Data
{
    public class RequestDataApi : DataApi, IRequestData
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
                    mediaType: "application/json")
                );
        }

        public async Task ChangeStatusRequest(Request request)
        {
            var putUrl = url + $"/{request.Id}";
            await client.PutAsync(
                    requestUri: putUrl,
                    content: new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
                        mediaType: "application/json")
                );
        }

        public async Task<Request> GetRequestById(int? id)
        {
            string json = await client.GetStringAsync(url + $"/{id}");

            return JsonConvert.DeserializeObject<Request>(json);
        }

        public async Task<IEnumerable<Request>> Requests()
        {
            string json = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<IEnumerable<Request>>(json);
        }
    }
}
