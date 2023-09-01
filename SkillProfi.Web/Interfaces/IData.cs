namespace SkillProfi.Web.Interfaces
{
    public interface IData<T>
    {
        /// <summary>
        /// Добавить объект в базу данных.
        /// </summary>
        /// <param name="model">Объект.</param>
        Task Add(T model);

        /// <summary>
        /// Удалить объект по Id.
        /// </summary>
        /// <param name="id">Id объекта.</param>
        /// <returns></returns>
        Task Delete(int? id);

        /// <summary>
        /// Обновить информацию по объекту.
        /// </summary>
        /// <param name="model">Объект.</param>
        Task Update(T model);

        /// <summary>
        /// Получить список объектов.
        /// </summary>
        /// <returns>Список объектов.</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Получить объект по Id.
        /// </summary>
        /// <param name="id">Id объекта.</param>
        /// <returns></returns>
        Task<T> GetById(int? id);
    }
}
