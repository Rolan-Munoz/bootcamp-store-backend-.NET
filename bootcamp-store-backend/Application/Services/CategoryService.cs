using System;
using AutoMapper;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Domain.Entities;
using bootcamp_store_backend.Domain.Persistence;
using bootcamp_store_backend.Domain.Services;

namespace bootcamp_store_backend.Application.Services
{
	public class CategoryService:GenericService<Category, CategoryDto>, ICategoryService
	{
        private ICategoryRepository _categoryRepository;
        private readonly IImageVerifier _imageVerifier;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IImageVerifier imageVerifier):base(categoryRepository, mapper)
        {
            _categoryRepository = categoryRepository;
            _imageVerifier = imageVerifier;
        }

        public override CategoryDto Insert(CategoryDto dto)
        {
            if (!_imageVerifier.IsImage(dto.Image))
                throw new InvalidImageException();
            return base.Insert(dto);
        }

        public override CategoryDto Update(CategoryDto dto)
        {
            if (!_imageVerifier.IsImage(dto.Image))
                throw new InvalidImageException();
            return base.Insert(dto);
        }

    }
}

