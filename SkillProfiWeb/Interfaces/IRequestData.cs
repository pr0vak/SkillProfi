using SkillProfiWeb.Models;

namespace SkillProfiWeb.Interfaces
{
    public interface IRequestData
    {
        Task<IEnumerable<Request>> Requests();
        Task<Request> GetRequestById(int? id);
        Task ChangeStatusRequest(Request request);
        Task Add(Request request);
    }
}
