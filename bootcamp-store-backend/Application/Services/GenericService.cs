using System;
using AutoMapper;
using bootcamp_store_backend.Domain.Persistence;

namespace bootcamp_store_backend.Application.Services
{
	public class GenericService<E,D>:IItemService<D>
        where E : class
        where D : class
	{
		
        protected readonly IGenericRepository<E> _repository;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<E> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        public List<D> GetAll()
        {
            var entities = _repository.GetAll();
            var dtos = _mapper.Map<List<D>>(entities);
            return dtos;
        }

        public D Get(long id)
        {
            var entity = _repository.GetById(id);
            return _mapper.Map<D>(entity);
        }

        public D Insert(D dto)
        {
            E entity = _mapper.Map<E>(dto);
            entity = _repository.Insert(entity);
            return _mapper.Map<D>(entity);
        }

        public D Update(D dto)
        {
            E entity = _mapper.Map<E>(dto);
            entity = _repository.Update(entity);
            return _mapper.Map<D>(entity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
	}


