using System;
using bootcamp_store_backend.Application;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Application.Services;
using bootcamp_store_backend.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bootcamp_store_backend.Infraestructure.Rest
{

    [Route("store/[controller]")]
    [ApiController]
    public class ItemsController : GenericCrudController<ItemDto>
    {

        private IItemService _itemService;
        private readonly ILogger<CategoriesController> _logger;

        public ItemsController(IItemService service, ILogger<CategoriesController> logger) : base(service)
        {
            _itemService = service;
            _logger = logger;
        }

        [NonAction]

        public override ActionResult<IEnumerable<ItemDto>> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult<PagedResponse<ItemDto>> Get([FromQuery] string? filter, [FromQuery] PaginationParameters paginationParameters)
        {
            try
            {
                PagedList<ItemDto> page = _itemService.GetItemsByCriteriaPaged(filter, paginationParameters);
                var response = new PagedResponse<ItemDto>
                {
                    CurrentPage = page.CurrentPage,
                    TotalPages = page.TotalPages,
                    PageSize = page.PageSize,
                    TotalCount = page.TotalCount,
                    Data = page
                };
                return Ok(response);
            } catch (MalformedFilterException)
            {
                return BadRequest();
            }
        }


        [HttpGet("/store/categories/{categoryId}/items")]
        [Produces("application/json")]
        public ActionResult<IEnumerable<ItemDto>> GetAllFromCategory(long categoryId)
        {
            var items = _itemService.GetAllByCategoryId(categoryId);
            return Ok(items);

        }

        public override ActionResult<ItemDto> Insert(ItemDto dto)
        {
            try
            {
                return base.Insert(dto);
            }
            catch (InvalidImageException)
            {
                _logger.LogInformation("Invalid image inserting item with {dto.Name}", dto.Name);
                return BadRequest();
            }
        }

        public override ActionResult<ItemDto> Update(ItemDto dto)
        {
            try
            {
                return base.Insert(dto);
            }
            catch (InvalidImageException)
            {
                _logger.LogInformation("Invalid image inserting item with {dto.Name}", dto.Name);
                return BadRequest();
            }
        }

    }
}

