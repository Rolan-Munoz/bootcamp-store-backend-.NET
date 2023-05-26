using System;
using bootcamp_store_backend.Application;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace bootcamp_store_backend.Infraestructure.Rest
{

    [Route("store/[controller]")]
    [ApiController]
    public class ItemController : GenericCrudController<ItemDto>
    {

        private IItemService _itemService;

        public ItemController(IItemService service) : base(service)
        {
            _itemService = service;
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
    }
}

