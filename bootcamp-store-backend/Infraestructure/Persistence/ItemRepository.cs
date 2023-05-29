using System;
using bootcamp_store_backend.Application;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Domain.Entities;
using bootcamp_store_backend.Domain.Persistence;
using bootcamp_store_backend.Infraestructure.Specs;
using Microsoft.EntityFrameworkCore;

namespace bootcamp_store_backend.Infraestructure.Persistence
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        private StoreContext _storeContext;
        private readonly ISpecificationParser<Item> _specificationParser;

        public ItemRepository(StoreContext storeContext, ISpecificationParser<Item> specificationParser) : base(storeContext)
        {
            _storeContext = storeContext;
            _specificationParser = specificationParser;
        }

        public List<ItemDto> GetByCategoryId(long categoryId)
        {
            var items = _dbSet.Where(i => i.CategoryId == categoryId)
                .Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    Image = i.Image,
                    CategoryId = categoryId,
                    CategoryName = i.Category.Name
                }).ToList();
            if (items == null)
            {
                return new List<ItemDto>();
            }
            return items.ToList();
        }

        public override Item GetById(long id)
        {
            var item = _storeContext.Items.Include(i => i.Category).SingleOrDefault(i => i.Id == id);
            if (item == null)
            {
                throw new ElementNotFoundException();
            }
            return item;
        }


        public override Item Insert(Item item)
        {
            _storeContext.Items.Add(item);
            _storeContext.SaveChanges();
            _storeContext.Entry(item).Reference(i => i.Category).Load();
            return item;
        }

        public override Item Update(Item item)
        {
            _storeContext.Items.Update(item);
            _storeContext.SaveChanges();
            _storeContext.Entry(item).Reference(i => i.Category).Load();
            return item;
        }

        public PagedList<ItemDto> GetItemsByCriteriaPaged(string? filter, PaginationParameters paginationParameters)
        {
            var items = _storeContext.Items.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                Specification<Item> specification = _specificationParser.ParseSpecification(filter);
                items = specification.ApplySpecification(items);
            }

            if (!string.IsNullOrEmpty(paginationParameters.Sort))
            {
                items = ApplySortOrder(items, paginationParameters.Sort);
            }

            var itemsDto = items.Select(i => new ItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                Image = i.Image,
                CategoryId = i.CategoryId,
                CategoryName = i.Category.Name
            });
          
                

            return PagedList<ItemDto>.ToPagedList(itemsDto, paginationParameters.PageNumber, paginationParameters.PageSize);
        }

       
    }
}

