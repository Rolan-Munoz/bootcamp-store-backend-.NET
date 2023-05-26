using System;
using bootcamp_store_backend.Application.Dtos;

namespace bootcamp_store_backend.Application.Services
{
    public interface IItemService : IGenericService<ItemDto>
    {
        List<ItemDto> GetAllByCategoryId(long categoryId);

        PagedList<ItemDto> GetItemsByCriteriaPaged(string? filter, PaginationParameters paginationParameters);
    }


}

