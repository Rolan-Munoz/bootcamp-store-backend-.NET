using System;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Application.Services;
using bootcamp_store_backend.Domain.Persistence;
using bootcamp_store_backend.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_store_backend.Infraestructure.Rest
{
	[Route("store/[controller]")]
	[ApiController]
	public class CategoriesController : GenericCrudController<CategoryDto>
	{
		private readonly ILogger<CategoriesController> _logger;


		public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger) : base(categoryService)
		{
			_logger = logger;
		}

		public override ActionResult<CategoryDto> Insert(CategoryDto dto)
		{
			try
			{
				return base.Insert(dto);
			}
			catch (InvalidImageException)
			{
				_logger.LogInformation("Invalid image inserting categoyr with {dto.Name} name", dto.Name);
				return BadRequest();
			}
		}

		public override ActionResult<CategoryDto> Update(CategoryDto dto)
		{
			try
			{
				return base.Update(dto);
			}
			catch (InvalidImageException)
			{
                _logger.LogInformation("Invalid image inserting categoyr with {dto.Name} name", dto.Name);
                return BadRequest();
            }
		}
	}

}

