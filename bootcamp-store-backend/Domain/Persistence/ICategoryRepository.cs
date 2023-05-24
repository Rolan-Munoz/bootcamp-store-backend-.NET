using bootcamp_store_backend.Domain.Entities;

namespace bootcamp_store_backend.Domain.Persistence
{
    public interface ICategoryRepository
    {
        void Delete(long id);

        List<Category> GetAll();

        Category GetById(long id);

        Category Insert(Category category);

        Category Update(Category category);
    }
}