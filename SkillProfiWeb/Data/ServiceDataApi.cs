using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace SkillProfiWeb.Data
{
    public class ServiceDataApi : DataApi, IData<Service>
    {
        public ServiceDataApi() 
        {
            url += "Services";

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
        }

        public async Task Add(Service service)
        {
            await client.PostAsync(
                    requestUri: url,
                    content: new StringContent(JsonConvert.SerializeObject(service), Encoding.UTF8,
                     "application/json")                    
                );
        }

        public async Task Delete(int? id)
        {
            var delUrl = url + $"/{id}";
            await client.DeleteAsync(delUrl);
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            string json = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<IEnumerable<Service>>(json);
        }

        public async Task<Service> GetById(int? id)
        {
            string json = await client.GetStringAsync(url + $"/{id}");

            return JsonConvert.DeserializeObject<Service>(json);
        }

        public async Task Update(Service service)
        {
            var putUrl = url + $"/{service.Id}";
            await client.PutAsync(
                    putUrl,
                    new StringContent(JsonConvert.SerializeObject(service), Encoding.UTF8, 
                        "application/json")
                );
        }
    }
}
