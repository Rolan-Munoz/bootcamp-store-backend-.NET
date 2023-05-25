using System;
using bootcamp_store_backend.Domain.Entities;
using bootcamp_store_backend.Domain.Persistence;

namespace bootcamp_store_backend.Infraestructure.Persistence
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private StoreContext _storeContext;

        public CategoryRepository(StoreContext storeContext) : base(storeContext)
        {
            _storeContext = storeContext;
        }
    }
}

