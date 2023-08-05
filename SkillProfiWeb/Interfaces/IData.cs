namespace SkillProfiWeb.Interfaces
{
    public interface IData<T>
    {
        Task Add(T model);
        Task Delete(int? id);
        Task Update(T model);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int? id);
    }
}
