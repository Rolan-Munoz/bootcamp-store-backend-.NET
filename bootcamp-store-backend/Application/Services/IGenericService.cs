using System;

namespace bootcamp_store_backend.Application.Services
{
	public interface IItemService<D> where D : class
	{
        

        void Delete(long id);

        List<D> GetAll();

        D Get(long id);

        D Insert(D dto);

        D Update(D dto);

    }
}

