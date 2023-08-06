using Newtonsoft.Json;
using SkillProfiWeb.Interfaces;
using SkillProfiWeb.Models;
using System.Text;

namespace SkillProfiWeb.Data
{
    public class ProjectDataApi : DataApi, IData<Project>
    {
        public ProjectDataApi()
        {
            url += "Projects";
        }

        public async Task Add(Project model)
        {
            await client.PutAsync(
                    url,
                    new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, 
                        "application/json")
                );
        }

        public async Task Delete(int? id)
        {
            var delUrl = url + $"/{id}";
            await client.DeleteAsync(delUrl);
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            string json = await client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<IEnumerable<Project>>(json);
        }

        public async Task<Project> GetById(int? id)
        {
            string json = await client.GetStringAsync(url + $"/{id}");

            return JsonConvert.DeserializeObject<Project>(json);
        }

        public async Task Update(Project model)
        {
            var putUrl = url + $"/{model.Id}";
            await client.PutAsync(
                    putUrl,
                    new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                        "application/json")
                );
        }
    }
}
