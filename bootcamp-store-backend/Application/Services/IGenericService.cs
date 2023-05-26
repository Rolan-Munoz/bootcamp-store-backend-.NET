using System;
using bootcamp_store_backend.Application.Dtos;

namespace bootcamp_store_backend.Application.Services
{
	public interface IGenericService<D> where D : class
	{
        

        void Delete(long id);

        List<D> GetAll();

        D Get(long id);

        D Insert(D dto);

        D Update(D dto);
        
    }
}

