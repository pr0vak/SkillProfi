using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace SkillProfiWeb.Data
{
    public class BlogDataApi : DataApi, IData<Blog>
    {
        public BlogDataApi() 
        {
            url += "Blogs";

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
        }

        public async Task Add(Blog model)
        {
            await client.PostAsync(
                    url,
                    new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                        "application/json")
                );
        }

        public async Task Delete(int? id)
        {
            await client.DeleteAsync(url + $"/{id}");
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            string json = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<IEnumerable<Blog>>(json);
        }

        public async Task<Blog> GetById(int? id)
        {
            var json = await client.GetStringAsync(url + $"/{id}");

            return JsonConvert.DeserializeObject<Blog>(json);
        }

        public async Task Update(Blog model)
        {
            await client.PutAsync(
                    url + $"/{model.Id}",
                    new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                        "application/json")
                );
        }
    }
}
