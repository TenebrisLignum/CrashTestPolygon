using Domain.Entities.Abstract;

namespace Data.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
