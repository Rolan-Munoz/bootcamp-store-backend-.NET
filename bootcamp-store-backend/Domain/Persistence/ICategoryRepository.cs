using bootcamp_store_backend.Domain.Entities;

namespace bootcamp_store_backend.Domain.Persistence
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
    }
}