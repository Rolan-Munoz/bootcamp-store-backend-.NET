using System;
using AutoMapper;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Domain.Entities;
using bootcamp_store_backend.Domain.Persistence;

namespace bootcamp_store_backend.Application.Services
{
    public class ItemService : GenericService<Item, ItemDto>, IItemService
    {
        public ItemService(IItemRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public List<ItemDto> GetAllByCategoryId(long categoryId)
        {
            var items = ((IItemRepository)_repository).GetByCategoryId(categoryId);
            return _mapper.Map<List<ItemDto>>(items);
        }
    }
}

