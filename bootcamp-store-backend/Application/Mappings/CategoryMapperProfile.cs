using System;
using AutoMapper;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Domain.Entities;

namespace bootcamp_store_backend.Application.Mappings
{
	public class CategoryMapperProfile:Profile
	{
		public CategoryMapperProfile()
		{
			CreateMap<Category, CategoryDto>();
			CreateMap<CategoryDto, Category>();

		}
	}
}

