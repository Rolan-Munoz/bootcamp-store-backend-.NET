using System;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_store_backend.Infraestructure.Rest
{
	[Route("store/[controller]")]
	[ApiController]
	public class CategoriesController:ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			this._categoryService = categoryService;
		}


		[HttpGet]
		public ActionResult<CategoryDto> GetCategories()
		{
			var categories = _categoryService.GetAllCategories();
			return Ok(categories);
		}
	}
}

