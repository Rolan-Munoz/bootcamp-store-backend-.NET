using System;
using AutoMapper;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Domain.Entities;
using bootcamp_store_backend.Domain.Persistence;

namespace bootcamp_store_backend.Application.Services
{
	public class CategoryService:ICategoryService
	{

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            _mapper = mapper;
        }

       

        public List<CategoryDto> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return categoriesDto;
        }

        public CategoryDto GetCategory(long id)
        {
            var category = _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public CategoryDto InsertCategory(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);
            category = _categoryRepository.Insert(category);
            return _mapper.Map<CategoryDto>(category);
        }

        public CategoryDto UpdateCategory(CategoryDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);
            category = _categoryRepository.Update(category);
            return _mapper.Map<CategoryDto>(category);
        }

        public void DeleteCategory(long id)
        {
            _categoryRepository.Delete(id);
        }
    }
}

