using System;
using AutoMapper;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Domain.Entities;
using bootcamp_store_backend.Domain.Persistence;

namespace bootcamp_store_backend.Application.Services
{
    public class ItemService : GenericService<Item, ItemDto>, IItemService
    {
        private IItemRepository _itemRepository;

        public ItemService(IItemRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _itemRepository = repository;
        }

        public List<ItemDto> GetAllByCategoryId(long categoryId)
        {
            var items = _itemRepository.GetByCategoryId(categoryId);
            return items;
        }

        public PagedList<ItemDto> GetItemsByCriteriaPaged(string? filter, PaginationParameters paginationParameters)
        {
            var items = _itemRepository.GetItemsByCriteriaPaged(filter, paginationParameters);
            return _mapper.Map<PagedList<ItemDto>>(items);
        }
    }
}

