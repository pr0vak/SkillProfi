using Newtonsoft.Json;
using SkillProfiWeb.Interfaces;
using SkillProfiWeb.Models;
using System.Text;

namespace SkillProfiWeb.Data
{
    public class BlogDataApi : DataApi, IData<Blog>
    {
        public BlogDataApi() 
        {
            url += "Blogs";
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
