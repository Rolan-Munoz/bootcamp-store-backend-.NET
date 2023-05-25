using System;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Domain.Entities;

namespace bootcamp_store_backend.Domain.Persistence
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        List<ItemDto> GetByCategoryId(long categoryId);
    }
}

