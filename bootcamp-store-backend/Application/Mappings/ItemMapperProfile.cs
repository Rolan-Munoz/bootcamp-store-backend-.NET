using System;
using AutoMapper;
using bootcamp_store_backend.Application.Dtos;
using bootcamp_store_backend.Domain.Entities;

namespace bootcamp_store_backend.Application.Mappings
{
	public class ItemMapperProfile:Profile
	{
		public ItemMapperProfile()
		{
			CreateMap<Item, ItemDto>();
			CreateMap<ItemDto, Item>();
			CreateMap<PagedList<Item>, PagedList<ItemDto>>()  
				.ConvertUsing((src, dest, context) =>
				 {
					 var items = context.Mapper.Map<List<ItemDto>>(src);
					 return new PagedList<ItemDto>(items, src.TotalCount, src.CurrentPage, src.PageSize);
				 });
		}
	}
}


