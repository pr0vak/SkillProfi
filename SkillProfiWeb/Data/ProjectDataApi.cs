using Newtonsoft.Json;
using SkillProfi.DAL.Models;
using SkillProfiWeb.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace SkillProfiWeb.Data
{
    public class ProjectDataApi : DataApi, IData<Project>
    {
        public ProjectDataApi()
        {
            url += "Projects";

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
        }

        public async Task Add(Project model)
        {
            await client.PostAsync(
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

            return JsonConvert.DeserializeObject<IEnumerable<Project>>(json)
                ?? new List<Project> { Project.CreateNullProject() };
        }

        public async Task<Project> GetById(int? id)
        {
            string json = await client.GetStringAsync(url + $"/{id}");

            return JsonConvert.DeserializeObject<Project>(json) ?? Project.CreateNullProject();
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
