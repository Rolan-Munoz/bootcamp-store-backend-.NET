using bootcamp_store_backend.Domain.Entities;

namespace bootcamp_store_backend.Domain.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        void Delete(long id);

        List<T> GetAll();

        T GetById(long id);

        T Insert(T entity);

        T Update(T entity);
    }
}