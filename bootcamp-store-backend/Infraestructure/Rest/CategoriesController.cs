using System;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Application.Services;
using bootcamp_store_backend.Domain.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_store_backend.Infraestructure.Rest
{
	[Route("store/[controller]")]
	[ApiController]
	public class CategoriesController : GenericCrudController<CategoryDto>
	{


		public CategoriesController(ICategoryService categoryService) : base(categoryService)
		{

		}
	}

}

