using System;
using AutoMapper;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Domain.Entities;
using bootcamp_store_backend.Domain.Persistence;

namespace bootcamp_store_backend.Application.Services
{
	public class CategoryService:GenericService<Category, CategoryDto>, ICategoryService
	{
        private ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper):base(categoryRepository, mapper)
        {
            _categoryRepository = categoryRepository;
        }

    }
}

