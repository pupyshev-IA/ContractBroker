namespace Abdt.ContractBroker.Repository.Abstractions
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Добавляет в хранилище новый элемент.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task Add(T item);

        /// <summary>
        /// Находит элемент по ключу в хранилище.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Возвращает найденный элемент или null если элемент не найден</returns>
        Task<T?> GetByKey(string key);
    }
}
